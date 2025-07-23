using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuoteKeeper.API.Data;
using QuoteKeeper.API.Dtos;
using QuoteKeeper.API.Models;
using QuoteKeeper.API.Services;
using QuoteKeeper.API.Extensions;
namespace QuoteKeeper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthService _authService;

        public AuthController(
            ApplicationDbContext context,
            IPasswordHasher<User> passwordHasher,
            AuthService authService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest request)
        {
            Console.WriteLine("Register endpoint was hit");

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Email is already registered.");
            }

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate Token after save user 
            var token = _authService.GenerateUserToken(user);


            return Ok(new AuthResponse
            {
                Token = token,
                Message = "User registered successfully.",
                User = new AuthUserDto
                {

                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }
            });

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine("Login endpoint was hit");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");

            }
            var isValid = _authService.VerifyPassword(user, request.Password, _passwordHasher);
            if (!isValid)
            {
                return Unauthorized("Invalid email or password.");

            }
            var token = _authService.GenerateUserToken(user);
            return Ok(new AuthResponse
            {
                Token = token,
                Message = "Login successful.",
                User = new AuthUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {

            return Ok(new { message = "You logged out." });
        }

    }
}

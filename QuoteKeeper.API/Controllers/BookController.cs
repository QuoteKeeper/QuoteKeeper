
using Microsoft.AspNetCore.Mvc;
using QuoteKeeper.API.Dtos;
using QuoteKeeper.API.Models;
using QuoteKeeper.API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace QuoteKeeper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public ActionResult<BookResponse> GetBook(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null)
                return NotFound();
            var response = new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                BarCode = book.BarCode,
                Author = book.Author,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                UserId = book.UserId,
                UserFullName = $"{book.User.FirstName} {book.User.LastName}"

            };

            return Ok(response);
        }
        [Authorize]
        [HttpPost]
        public ActionResult<Book> Create([FromBody] BookRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 🔐 استخراج UserId من التوكن والتحقق من صحته
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _));

            if (userIdClaim == null)
                return Unauthorized(new { message = "Invalid or missing user ID in token." });

            int userId = int.Parse(userIdClaim.Value);

            Console.WriteLine("✅ Authenticated user ID: " + userId);

            try
            {
                var createdBook = _bookService.Create(request, userId);
                var response = new BookResponse
                {
                    Id = createdBook.Id,
                    Title = createdBook.Title,
                    BarCode = createdBook.BarCode,
                    Author = createdBook.Author,
                    Description = createdBook.Description,
                    PublishedDate = createdBook.PublishedDate,
                    UserId = createdBook.UserId,
                    UserFullName = $"{createdBook.User.FirstName} {createdBook.User.LastName}"


                };


                return CreatedAtAction(nameof(GetBook), new { id = response.Id }, response);

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }






        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var book = _bookService.GetAll();
            return Ok(book);
        }
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, [FromBody] UpdateBookRequest request)
        {
            if (!ModelState.IsValid) // to ensure that vaule is valid (VALIDATION TO JSON)
                return BadRequest(ModelState);

            var update = _bookService.Update(id, request);
            if (!update)
                return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deleted = _bookService.Delete(id);
            if (!deleted)

                return NotFound();
            return NoContent();
        }

    }
}
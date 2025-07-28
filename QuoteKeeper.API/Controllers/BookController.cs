
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
        public ActionResult<IEnumerable<BookResponse>> GetAllBooks()
        {
            var books = _bookService.GetAll();

            foreach (var b in books)
            {
                Console.WriteLine($"DEBUG BookId: {b.Id}, User: {(b.User != null ? b.User.FirstName + " " + b.User.LastName : "NULL")}");
            }
            var response = books.Select(book => new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                BarCode = book.BarCode,
                Author = book.Author,
                Description = book.Description,
                PublishedDate = book.PublishedDate,
                UserId = book.UserId,
                UserFullName = book.User != null ? $"{book.User.FirstName} {book.User.LastName}" : null


            });
            return Ok(response);
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

            var updateBook = _bookService.GetById(id);
            var response = new UpdateBookRequest
            {
                Id = updateBook.Id,
                Title = updateBook.Title,
                BarCode = updateBook.BarCode,
                Author = updateBook.Author,
                Description = updateBook.Description,
                PublishedDate = updateBook.PublishedDate,

            };
            return Ok(response);
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
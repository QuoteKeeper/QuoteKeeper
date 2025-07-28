using QuoteKeeper.API.Models;
using QuoteKeeper.API.Dtos;
using Microsoft.VisualBasic;
using QuoteKeeper.API.Services;
using QuoteKeeper.API.Data;
using Microsoft.EntityFrameworkCore;


namespace QuoteKeeper.API.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books
            .Include(b => b.User)
            .AsNoTracking()
            .ToList();
        }
        public Book? GetById(int id)
        {
            return _context.Books
            .Include(b => b.User)
            .FirstOrDefault(b => b.Id == id);
        }


        public Book Create(BookRequest request, int userId)
        {
            var titleExists = _context.Books
        .Any(b => b.Title.ToLower() == request.Title.ToLower());

            if (titleExists)
                throw new InvalidOperationException("⚠️ The book title is already in use");


            var barcodeExists = _context.Books
                .Any(b => b.BarCode.ToLower() == request.BarCode.ToLower());

            if (barcodeExists)
                throw new InvalidOperationException("⚠️ The barcode is already in use.");

            var book = new Book
            {

                Title = request.Title,
                BarCode = request.BarCode,
                Author = request.Author,
                Description = request.Description,
                PublishedDate = request.PublishedDate,
                UserId = userId,
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            return _context.Books
         .Include(b => b.User)
         .First(b => b.Id == book.Id);
        }

        public bool Update(int id, UpdateBookRequest request)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null) return false;

            if (request.Title != null)
                book.Title = request.Title;

            if (request.BarCode != null)
                book.BarCode = request.BarCode;
            if (request.Author != null)
                book.Author = request.Author;
            if (request.Description != null)
                book.Description = request.Description;
            if (request.PublishedDate.HasValue)
                book.PublishedDate = request.PublishedDate.Value;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(_b => _b.Id == id);
            if (book == null) return false;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }


    }
}
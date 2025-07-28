using QuoteKeeper.API.Data;
using QuoteKeeper.API.Dtos;
using QuoteKeeper.API.Models;
using Microsoft.EntityFrameworkCore;


namespace QuoteKeeper.API.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly ApplicationDbContext _context;
        public QuoteService(ApplicationDbContext context)
        {
            _context = context;

        }
        public QuoteResponse CreateQuote(QuoteRequest request, int userId)
        {
            var quote = new Quote
            {
                Text = request.Text,
                QuoteType = request.QuoteType,
                BookId = request.BookId,
                UserId = userId,
            };
            _context.Quotes.Add(quote);
            _context.SaveChanges();

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }


            return new QuoteResponse
            {
                Id = quote.Id,
                Text = quote.Text,
                QuoteType = quote.QuoteType,
                UserId = userId,
                BookId = quote.BookId,
                CreatedBy = user.FirstName
            };
        }
    }
}
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
            var book = _context.Books.FirstOrDefault(u => u.Id == quote.BookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }


            return new QuoteResponse
            {
                Id = quote.Id,
                Text = quote.Text,
                QuoteType = quote.QuoteType,
                UserId = userId,
                BookId = quote.BookId,
                BookTitle = book.Title,
                CreatedBy = user.FirstName
            };
        }
        public IEnumerable<QuoteResponse> GetAllQuotes()
        {
            var quotes = _context.Quotes
            .Include(q => q.User)
            .Include(q => q.Book)
            .Include(q => q.FavoritedByUsers)
            .ThenInclude(f => f.User)
            .ToList();
            return quotes.Select(q => new QuoteResponse
            {
                Id = q.Id,
                Text = q.Text,
                QuoteType = q.QuoteType,
                UserId = q.UserId,
                BookId = q.BookId,
                BookTitle = q.Book.Title,
                CreatedBy = q.User.FirstName,
                FavoritedByUsers = q.FavoritedByUsers.Select(f => new QuoteFavoriteResponse
                {
                    QuoteId = q.Id,
                    Text = q.Text,
                    QuoteType = q.QuoteType,
                    BookId = q.BookId
                }).ToList()

            });
        }

        public IEnumerable<QuoteFavoriteResponse> GetFavoriteQuotesByUser(int userId)
        {
            var favorites = _context.UserFavoriteQuotes
            .Include(f => f.Quote)
            .Where(f => f.UserId == userId)
            .Select(f => new QuoteFavoriteResponse
            {
                QuoteId = f.Quote.Id,
                Text = f.Quote.Text,
                QuoteType = f.Quote.QuoteType,
                BookId = f.Quote.BookId,
            })
            .ToList();
            return favorites;

        }

        public IEnumerable<UserFavoriteResponse> GetUsersWhoFavoritedQuote(int quoteId)
        {
            var favoriteUsers = _context.UserFavoriteQuotes
            .Where(f => f.QuoteId == quoteId)
            .Include(f => f.User)
            .Select(f => f.User)
            .Where(u => u != null)
            .Select(u => new UserFavoriteResponse
            {
                UserId = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName
            })
            .ToList();
            return favoriteUsers;
        }


        public bool AddToFavorites(QuoteFavoriteRequest request)
        {
            var alreadyFavorited = _context.UserFavoriteQuotes
            .Any(f => f.UserId == request.UserId && f.QuoteId == request.QuoteId);
            if (alreadyFavorited)
                return false;

            var favorite = new UserFavoriteQuote
            {
                UserId = request.UserId,
                QuoteId = request.QuoteId,
            };

            _context.UserFavoriteQuotes.Add(favorite);
            _context.SaveChanges();
            return true;
        }


        public bool RemoveFromFavorites(QuoteFavoriteRequest request)
        {
            var favorite = _context.UserFavoriteQuotes
            .FirstOrDefault(f => f.UserId == request.UserId && f.QuoteId == request.QuoteId);

            if (favorite == null)
                return false;

            _context.UserFavoriteQuotes.Remove(favorite);
            _context.SaveChanges();
            return true;
        }



    }
}
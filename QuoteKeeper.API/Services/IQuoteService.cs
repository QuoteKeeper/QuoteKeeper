using QuoteKeeper.API.Dtos;
using QuoteKeeper.API.Models;
using QuoteKeeper.API.Services;


namespace QuoteKeeper.API.Services
{
    public interface IQuoteService
    {
        QuoteResponse CreateQuote(QuoteRequest request, int userId);
        IEnumerable<QuoteResponse> GetAllQuotes();
        IEnumerable<QuoteFavoriteResponse> GetFavoriteQuotesByUser(int userId);
        IEnumerable<UserFavoriteResponse> GetUsersWhoFavoritedQuote(int quoteId);
        bool AddToFavorites(QuoteFavoriteRequest request);
        bool RemoveFromFavorites(QuoteFavoriteRequest request);
        int GetFavoriteCount(int quoteId);
    }
}
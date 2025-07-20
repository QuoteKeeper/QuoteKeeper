using QuoteKeeper.API.Dtos;

namespace QuoteKeeper.API.Dtos
{
    public class QuoteFavoriteRequest
    {
        public int UserId { get; set; }
        public int QuoteId { get; set; }
    }
}
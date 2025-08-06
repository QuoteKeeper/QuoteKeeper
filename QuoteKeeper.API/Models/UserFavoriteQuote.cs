namespace QuoteKeeper.API.Models
{
    public class UserFavoriteQuote
    {
        public int UserId { get; set; }
        public User User { get; set; } = null;

        public int QuoteId { get; set; }
        public Quote Quote { get; set; } = null;
    }
}
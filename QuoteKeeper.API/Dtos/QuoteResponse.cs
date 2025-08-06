using QuoteKeeper.API.Dtos;
using QuoteKeeper.API.Models;


namespace QuoteKeeper.API.Dtos
{
    public class QuoteResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = null!;
        public QuoteType QuoteType { get; set; }
        public string CreatedBy { get; set; }

        //public List<QuoteFavoriteResponse> FavoritedByUsers { get; set; } = new();
    }
}

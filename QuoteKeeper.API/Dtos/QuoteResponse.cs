using QuoteKeeper.API.Dtos;


namespace QuoteKeeper.API.Dtos
{
    public class QuoteResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public int UserId { get; set; }
        public int BookId { get; set; }
        public QuoteType QuoteType { get; set; }

        public List<QuoteFavoriteResponse> FavoritedByUsers { get; set; } = new();
    }
}

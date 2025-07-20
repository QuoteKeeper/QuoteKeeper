using QuoteKeeper.API.Dtos;

namespace QuoteKeeper.API.Dtos
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string BarCode { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
    }
}

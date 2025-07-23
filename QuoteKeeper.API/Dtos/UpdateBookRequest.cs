using QuoteKeeper.API.Dtos;
namespace QuoteKeeper.API.Dtos
{
    public class UpdateBookRequest
    {
        public string? Title { get; set; }
        public string? BarCode { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}

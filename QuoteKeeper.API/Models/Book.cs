using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteKeeper.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BarCode { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}

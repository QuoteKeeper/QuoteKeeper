
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuoteKeeper.API.Dtos;


namespace QuoteKeeper.API.Dtos
{
    public class BookRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string BarCode { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime PublishedDate { get; set; }
    }
}

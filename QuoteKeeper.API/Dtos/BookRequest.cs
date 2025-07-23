
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuoteKeeper.API.Dtos;
using Microsoft.EntityFrameworkCore;


namespace QuoteKeeper.API.Dtos
{
    [Index(nameof(Title), IsUnique = true)]
    [Index(nameof(BarCode), IsUnique = true)]
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
        [MaxLength(2500)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime PublishedDate { get; set; }
    }
}

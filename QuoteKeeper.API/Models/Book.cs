using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace QuoteKeeper.API.Models
{
    [Index(nameof(Title), IsUnique = true)]
    [Index(nameof(BarCode), IsUnique = true)]
    public class Book
    {

        [Key]
        public int Id { get; set; }

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


        public DateTime PublishedDate { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

    }
}

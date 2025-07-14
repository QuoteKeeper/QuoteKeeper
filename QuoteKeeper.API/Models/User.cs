using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteKeeper.API.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;

        public ICollection<Quote> Quotes { get; set; } = new List<Quote>();
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<UserFavoriteQuote> FavoriteQuotes { get; set; } = new List<UserFavoriteQuote>();
    }
}

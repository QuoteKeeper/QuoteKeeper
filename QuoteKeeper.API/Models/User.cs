using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using QuoteKeeper.API.Models;
namespace QuoteKeeper.API.Models
{

    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; } = null!;

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

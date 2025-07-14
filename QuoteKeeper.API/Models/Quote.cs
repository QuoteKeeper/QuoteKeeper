using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteKeeper.API.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = null!;

        [Required]
        public int? UserId { get; set; }
        public User? User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [Required]
        public QuoteType? QuoteType { get; set; }
        public ICollection<UserFavoriteQuote> FavoritedByUsers { get; set; } = new List<UserFavoriteQuote>();

    }
}

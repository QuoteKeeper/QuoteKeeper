using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuoteKeeper.API.Dtos;

namespace QuoteKeeper.API.Dtos
{
    public class QuoteRequest
    {
        [Required]
        [MaxLength(200)]
        public string Text { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public QuoteType QuoteType { get; set; }
    }
}

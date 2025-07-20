using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuoteKeeper.API.Dtos;

namespace QuoteKeeper.API.Dtos

// To show any Quote liked this user
{
    public class QuoteFavoriteResponse
    {
        public int QuoteId { get; set; }
        public string Text { get; set; } = null!;
        public QuoteType QuoteType { get; set; }
        public int BookId { get; set; }
    }
}

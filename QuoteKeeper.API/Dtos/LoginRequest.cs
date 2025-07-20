using System.ComponentModel.DataAnnotations;
using QuoteKeeper.API.Dtos;

namespace QuoteKeeper.API.Dtos
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

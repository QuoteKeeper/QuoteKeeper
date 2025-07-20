using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using QuoteKeeper.API.Dtos;

namespace QuoteKeeper.API.Dtos
{
    public class AuthRequest
    {
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

    }
}

using System.ComponentModel.DataAnnotations;

namespace QuoteKeeper.API.Config
{
    public class JwtSettings
    {
        [Required(ErrorMessage = "Key is required.")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Issuer is required.")]
        public string Issuer { get; set; }

        [Required(ErrorMessage = "Audience is required.")]
        public string Audience { get; set; }

        [Range(1, 1440, ErrorMessage = "ExpiresInMinutes must be between 1 and 1440.")]
        public int ExpiresInMinutes { get; set; } = 60;
    }
}

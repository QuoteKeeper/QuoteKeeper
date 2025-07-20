using QuoteKeeper.API.Dtos;

namespace QuoteKeeper.API.Dtos
{
    public class AuthUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}

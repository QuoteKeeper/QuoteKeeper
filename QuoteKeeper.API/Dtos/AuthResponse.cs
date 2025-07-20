using QuoteKeeper.API.Dtos;


namespace QuoteKeeper.API.Dtos
{
    public class AuthResponse
    {

        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AuthUserDto User { get; set; } = new AuthUserDto();


    }
}

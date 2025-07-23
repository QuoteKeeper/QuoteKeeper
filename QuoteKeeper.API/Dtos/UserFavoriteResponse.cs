

using QuoteKeeper.API.Dtos;
namespace QuoteKeeper.API.Dtos

// To show any user like this Quote 
{
    public class UserFavoriteResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}

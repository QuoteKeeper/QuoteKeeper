using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuoteKeeper.API.Dtos;



namespace QuoteKeeper.API.Dtos
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string Message { get; set; }

        public List<QuoteResponse> Quotes { get; set; } = new();
    }
}

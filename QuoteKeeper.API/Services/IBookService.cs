using QuoteKeeper.API.Models;
using QuoteKeeper.API.Dtos;
using QuoteKeeper.API.Services;

namespace QuoteKeeper.API.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int Id); // ? To return Null if dont fiind book 
        Book Create(BookRequest request, int userId);
        bool Update(int id, UpdateBookRequest request);
        bool Delete(int id);
    }

}
using QuoteKeeper.API.Models;
using QuoteKeeper.API.Dtos;

namespace QuoteKeeper.API.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int Id); // ? To return Null if dont fiind book 
        Book Create(BookRequest request);
        bool Update(int id, BookRequest request);
    }

}
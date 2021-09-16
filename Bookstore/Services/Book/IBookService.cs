using Bookstore.Data.Respository;
using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Services.Book
{
    public interface IBookService:IRepository<BookEntity>
    {
        Task<IEnumerable<BookEntity>> GetAllBooksAsync();
        Task<BookEntity> GetBookByIdAsync(int bookId);
        void CreateBook(BookEntity bookEntity);
        void UpdateBook(BookEntity bookEntity);
        void DeleteBook(BookEntity bookEntity);
    }
}

using Bookstore.Infrastructure.Data.Context;
using Bookstore.Infrastructure.Data.Respository;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Services.Book
{
    public class BookService:Repository<BookEntity>, IBookService
    {
        public BookService(AppDbContext context) : base(context)
        {

        }

        public void CreateBook(BookEntity bookEntity)
        {
            Create(bookEntity);
        }

        public void DeleteBook(BookEntity bookEntity)
        {
            Delete(bookEntity);
        }

        public async Task<IEnumerable<BookEntity>> GetAllBooksAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<BookEntity> GetBookByIdAsync(int bookId)
        {
            return await FindByCondition(b => b.BookId.Equals(bookId))
             .Include(a => a.Author)
             .FirstOrDefaultAsync();
        }

        public void UpdateBook(BookEntity bookEntity)
        {
            Update(bookEntity);
        }
    }
}

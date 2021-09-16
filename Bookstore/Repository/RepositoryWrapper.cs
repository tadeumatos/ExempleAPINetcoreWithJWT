using Bookstore.Infrastructure.Data.Context;
using Bookstore.Services.Author;
using Bookstore.Services.Book;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AuthorService _authorService;
        private BookService _bookService;
        public AppDbContext _context;

        public RepositoryWrapper(AppDbContext context)
        {
            _context = context;

        }
        public IAuthorService AuthorService
        {
            get
            {
                return _authorService = _authorService ?? new AuthorService(_context);
            }
        }

        public IBookService BookService
        {
            get
            {
                return _bookService = _bookService ?? new BookService(_context);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}

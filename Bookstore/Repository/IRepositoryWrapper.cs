using Bookstore.Services.Author;
using Bookstore.Services.Book;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public interface IRepositoryWrapper
    {
        IAuthorService AuthorService { get; }
        IBookService BookService { get; }
        Task SaveAsync();
        void Dispose();
    }
}

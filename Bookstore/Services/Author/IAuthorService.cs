using Bookstore.Data.Respository;
using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Services.Author
{
   public interface IAuthorService:IRepository<AuthorEntity>
    {
        Task<IEnumerable<AuthorEntity>> GetAllAuthorAsync();
        Task<AuthorEntity> GetAuthorByIdAsync(int authorId);
        Task<IEnumerable<AuthorEntity>> GetAllAuthorBooksAsync();
        Task<AuthorEntity> GetAuthorBookByIdAsync(int authorId);
        void CreateAuthor(AuthorEntity authorEntity);
        void UpdateAuthor(AuthorEntity authorEntity);
        void DeleteAuthor(AuthorEntity authorEntity);

    }
}

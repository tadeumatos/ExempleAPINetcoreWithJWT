using Bookstore.Infrastructure.Data.Context;
using Bookstore.Infrastructure.Data.Respository;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Services.Author
{
    public class AuthorService:Repository<AuthorEntity>, IAuthorService
    {
        public AuthorService(AppDbContext context) : base(context)
        {

        }

        public void CreateAuthor(AuthorEntity authorEntity)
        {
            Create(authorEntity);
        }

        public void DeleteAuthor(AuthorEntity authorEntity)
        {
            Delete(authorEntity);
        }

        public async Task<IEnumerable<AuthorEntity>> GetAllAuthorAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<IEnumerable<AuthorEntity>> GetAllAuthorBooksAsync()
        {
            return await GetAll().Include(x => x.Books).ToListAsync();
        }

        public async Task<AuthorEntity> GetAuthorBookByIdAsync(int authorId)
        {
            return await FindByCondition(a =>a.AuthorId.Equals(authorId))
             .Include(b => b.Books)
             .FirstOrDefaultAsync();
        }

        public async Task<AuthorEntity> GetAuthorByIdAsync(int authorId)
        {
            return await FindByCondition(a =>a.AuthorId.Equals(authorId))
            .FirstOrDefaultAsync();
        }

        public void UpdateAuthor(AuthorEntity authorEntity)
        {
            Update(authorEntity);
        }
    }
}

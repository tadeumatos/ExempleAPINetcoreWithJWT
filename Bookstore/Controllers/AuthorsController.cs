using Bookstore.Models;
using Bookstore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;

        public AuthorsController(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _wrapper.AuthorService.GetAllAuthorAsync());
        }

        [HttpGet("{id}", Name = "GetAuthorId")]
        public async Task<IActionResult> Get(int id)
        {
            var author = await _wrapper.AuthorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpGet("GetAuthorBooks")]
        public async Task<IActionResult> GetAuthorAllBooks()
        {
            var authorBooks = await _wrapper.AuthorService.GetAllAuthorBooksAsync();

            if (authorBooks == null)
            {
                return NotFound();
            }

            return Ok(authorBooks);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorEntity author)
        {
            _wrapper.AuthorService.CreateAuthor(author);
            await _wrapper.SaveAsync();

            return new CreatedAtRouteResult("GetAuthorId", new { id = author.AuthorId }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorEntity author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            _wrapper.AuthorService.UpdateAuthor(author);
            await _wrapper.SaveAsync();
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _wrapper.AuthorService.GetAuthorByIdAsync(id); 

            if (author == null)
            {
                return NotFound();
            }

            _wrapper.AuthorService.DeleteAuthor(author);
            await _wrapper.SaveAsync();
            return Ok(author);

        }

    }
}

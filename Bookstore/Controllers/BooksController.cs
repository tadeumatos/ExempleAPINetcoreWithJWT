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
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        public BooksController(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _wrapper.BookService.GetAllBooksAsync());
        }

        [HttpGet("{id}", Name = "GetBookId")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _wrapper.BookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookEntity book)
        {
            _wrapper.BookService.CreateBook(book);
            await _wrapper.SaveAsync();

            return new CreatedAtRouteResult("GetBookId", new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookEntity book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _wrapper.BookService.UpdateBook(book);
            await _wrapper.SaveAsync();
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _wrapper.BookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _wrapper.BookService.DeleteBook(book);
            await _wrapper.SaveAsync();
            return Ok(book);

        }
    }
}

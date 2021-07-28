using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_core_api.Models;
using net_core_api.Repositories;

namespace net_core_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        } 

        [HttpGet("all-books")]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetBooks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await _bookRepository.GetBook(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
        {
            var newBook = await _bookRepository.AddBook(book);
            // return CreatedAtAction(nameof(GetBook), new {id = newBook.Id}, newBook);
            return newBook;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if(id != book.Id)
            {
                return BadRequest();
            }
            await _bookRepository.Update(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var bookToDelete = await _bookRepository.GetBook(id);
            if(bookToDelete == null)
            {
                return NotFound();
            }
            
            await _bookRepository.Delete(bookToDelete.Id);
            return NoContent();
        }
    }
}
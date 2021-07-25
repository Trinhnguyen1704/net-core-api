using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net_core_api.Models;

namespace net_core_api.Repositories
{
    public class BookRepository : IBookRepository
    {
        //database context
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        // Add new book
        public async Task<Book> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();  // async

            return book;
        }

        // Delete book
        public async Task Delete(int id)
        {
            var bookDeleted = await GetBook(id); // get book to delete

            _context.Books.Remove(bookDeleted);
            await _context.SaveChangesAsync();
        }

        // Get Book with Id
        public async Task<Book> GetBook(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
        }

        //Get book list
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        //Update book
        public async Task Update(Book book)
        {
            var bookToUpdate = await GetBook(book.Id);

            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.IsAvailable = book.IsAvailable;
            bookToUpdate.Description = book.Description;

            await _context.SaveChangesAsync();
        }
    }
}
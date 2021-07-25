using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_core_api.Models;

namespace net_core_api.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks(); // IEnumerable : một interface collection, duyệt phần tử bằng foreach.
        Task<Book> GetBook(int id);
        Task<Book> AddBook(Book book);
        Task Update(Book book);
        Task Delete(int id);
    }
}
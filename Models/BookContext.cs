using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace net_core_api.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books {get; set;}
    }
}

// nạp chồng phương thức OnConfiguring, pt này được chạy khi đối tường DbContext được tạo ra
// cấu hình cơ sở dữ liệu
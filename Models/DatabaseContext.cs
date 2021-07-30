using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace net_core_api.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassStudent>()
                .HasOne(c => c.Student)
                .WithMany(cs => cs.ClassStudents)
                .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<ClassStudent>()
                .HasOne(c => c.Class)
                .WithMany(cs => cs.ClassStudents)
                .HasForeignKey(c => c.ClassId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students {get; set;}
        public DbSet<Class> Classes {get; set;}
        public DbSet<ClassStudent> ClassStudents{get; set;}
    }
}
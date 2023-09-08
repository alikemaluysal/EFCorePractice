using _8_Tracking.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_Tracking
{
    internal class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDb;Integrated Security=True;");

            //UseQueryTrackingBehavior
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var authors = new List<Author>()
            {
                new Author(){ Id = 1, FirstName = "Robert Cecil", LastName = "Martin" },
                new Author(){ Id = 2, FirstName = "Martin", LastName = "Fowler" }
            };

            var books = new List<Book>()
            {
                new Book(){ Id = 1, Title = "Clean Code", AuthorId = 1},
                new Book(){ Id = 2, Title = "Clean Architecture", AuthorId = 1},
                new Book(){ Id = 3, Title = "Refactoring", AuthorId = 2},
                new Book(){ Id = 4, Title = "Analysis Patterns", AuthorId = 2},
            };


            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);
        }
    }
}

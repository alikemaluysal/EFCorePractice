using Microsoft.EntityFrameworkCore;

namespace _8_Tracking
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            LibraryDbContext context = new();
            await context.Database.EnsureCreatedAsync();

            //AsNoTracking
            var authors = await context.Authors.AsNoTracking().ToListAsync();

            //AsNoTrackingWithIdentityResolution
            var books = await context.Books.Include(b => b.Author).AsNoTrackingWithIdentityResolution().ToListAsync();

            //AsTracking
            authors = await context.Authors.AsTracking().ToListAsync();


         }
    }


}   
using _4_Code_First.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _4_Code_First
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var context = new ECommerceDbContext();
            await context.Database.MigrateAsync();
        }
    }
}
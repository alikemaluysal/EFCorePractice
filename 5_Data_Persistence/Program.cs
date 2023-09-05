using _5_Data_Persistence.Contexts;
using _5_Data_Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace _5_Data_Persistence
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await Add();
            //await Update();
            await Delete();
        }

        static async Task Add()
        {
            var context = new ECommerceDbContext();

            var product = new Product()
            {
                Name = "Product A",
                Price = 120,
                Stock = 15

            };

            var product2 = new Product()
            {
                Name = "Product B",
                Price = 300,
                Stock = 10

            };

            var product3 = new Product()
            {
                Name = "Product C",
                Price = 240,
                Stock = 25

            };

            var product4 = new Product()
            {
                Name = "Product D",
                Price = 240,
                Stock = 25

            };

            var product5 = new Product()
            {
                Name = "Product E",
                Price = 240,
                Stock = 25

            };

            var products = new List<Product>()
            {
                new Product()
                {
                    Name = "Product F",
                    Price = 320,
                    Stock = 30
                },

                new Product()
                {
                    Name = "Product G",
                    Price = 450,
                    Stock = 5
                }
        };


            await context.AddAsync(product);

            await context.Products.AddAsync(product2);

            context.Entry(product3).State = EntityState.Added;

            await context.Products.AddRangeAsync(product4, product5);

            await context.Products.AddRangeAsync(products);

            context.SaveChanges();

            Console.WriteLine(product5.Id);
        }

        static async Task Update()
        {
            var context = new ECommerceDbContext();

            var product = context.Products.FirstOrDefault(p => p.Id == 2);

            product.Price = 500;


            var product2 = new Product()
            {
                Id = 3,
                Name = "Product X",
                Price = 220,
                Stock = 12

            };

            context.Products.Update(product2);

            var product3 = new Product()
            {
                Id = 4,
                Name = "Product Y",
                Price = 520,
                Stock = 120
            };

            context.Entry(product3).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        static async Task Delete()
        {
            var context = new ECommerceDbContext();

            var product = context.Products.FirstOrDefault(p => p.Id == 1);

            context.Products.Remove(product);

            var product2 = new Product()
            {
                Id = 2,
            };

            context.Products.Remove(product2);

            var product3 = new Product()
            {
                Id = 3,
            };

            context.Entry(product3).State = EntityState.Deleted;


            var products = await context.Products.Where(p => p.Id >= 4 && p.Id <= 7).ToListAsync();

            context.Products.RemoveRange(products);

            await context.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Z_Northwind.Contexts;
using Z_Northwind.Entities;

namespace _6_Querying
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Querying();
        }

        static async Task Querying()
        {
            var context = new NorthwindContext();

            //Method Syntax
            IEnumerable<Product> products = await context.Products.ToListAsync();

            //Query Syntax
            IEnumerable<Product> products2 = await(
                            from product in context.Products
                            select product
                            ).ToListAsync();

            //Deferred Execution
            int minProductId = 10;

            IQueryable<Product> query = from product in context.Products
                                        where product.ProductId > minProductId
                                        select product;

            minProductId = 30;

            IEnumerable<Product> products3 = await query.ToListAsync();


            foreach (var item in products3)
            {
                Console.WriteLine(item.ProductId);
            }
        }
    }
}
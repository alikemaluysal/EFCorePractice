using Microsoft.EntityFrameworkCore;
using Z_Northwind.Contexts;
using Z_Northwind.Entities;

namespace _6_Querying
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await Querying();
            //await Where();
            //await OrderBy();
            //await ThenBy();
            //await Single();
            //await SingleOrDefault();
            //await First();
            //await FirstOrDefault();
            //await Last();
            //await LastOrDefault();
            await Find();

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

        static async Task Where()
        {
            var context = new NorthwindContext();

            //Method Syntax
            var products = await context.Products.Where(p => p.ProductId > 20 && p.ProductId < 50).ToListAsync();

            //Query Syntax
            var query = from product in context.Products
                          where product.ProductId > 5 && product.ProductName.StartsWith("L")
                        select product;

            var products2 = await query.ToListAsync();


            foreach (var item in products2)
            {
                Console.WriteLine($"{item.ProductId} - {item.ProductName}");
            }
            Console.WriteLine();
        }

        static async Task OrderBy()
        {
            var context = new NorthwindContext();

            //Method Syntax
            var products = await context.Products.OrderBy(p => p.ProductName).ToListAsync();
            //var products = await context.Products.OrderByDescending(p => p.ProductName).ToListAsync();

            //Query Syntax
            var query = from product in context.Products
                        orderby product.ProductName descending
                        select product;

            var products2 = await query.ToListAsync();


            foreach (var item in products2)
            {
                Console.WriteLine($"{item.ProductName}");
            }
            Console.WriteLine();
        }

        static async Task ThenBy()
        {
            var context = new NorthwindContext();

            //Method Syntax
            var products = await context.Products.OrderBy(p => p.UnitPrice).ThenBy(p => p.ProductName).ToListAsync();
            //var products = await context.Products.OrderByDescending(p => p.UnitPrice).ThenByDescending(p => p.ProductName).ToListAsync();

            //Query Syntax
            var query = from product in context.Products
                        orderby product.UnitPrice descending, product.ProductName ascending 
                        select product;

            var products2 = await query.ToListAsync();


            foreach (var item in products2)
            {
                Console.WriteLine($"{item.ProductName} - {item.UnitPrice}");
            }
            Console.WriteLine();
        }
       
        static async Task Single()
        {
            var context = new NorthwindContext();

            Product? product;

            //Single element
            product = await context.Products.SingleAsync(u => u.ProductId == 55);

            //No elements
            //product = await context.Products.SingleAsync(u => u.ProductId == 5555);

            //More than one element
            //product = await context.Products.SingleAsync(u => u.ProductId > 55);


            Console.WriteLine($"{product.ProductId} - {product.ProductName}");

        }
        static async Task SingleOrDefault()
        {
            var context = new NorthwindContext();
            Product? product;

            //Single element
            product = await context.Products.SingleOrDefaultAsync(u => u.ProductId == 55);

            //No elements
            //product = await context.Products.SingleOrDefaultAsync(u => u.ProductId == 5555);

            //More than one element
            //product = await context.Products.SingleOrDefaultAsync(u => u.ProductId > 55);

            Console.WriteLine($"{product.ProductId} - {product.ProductName}");

        }

        static async Task First()
        {
            var context = new NorthwindContext();
            Product? product;

            //Single element
            product = await context.Products.FirstAsync(u => u.ProductId == 55);

            //No elements
            //product = await context.Products.FirstAsync(u => u.ProductId == 5555);

            //More than one element
            //product = await context.Products.FirstAsync(u => u.ProductId > 55);

            Console.WriteLine($"{product.ProductId} - {product.ProductName}");

        }
        static async Task FirstOrDefault()
        {
            var context = new NorthwindContext();
            Product? product;

            //Single element
            product = await context.Products.FirstOrDefaultAsync(u => u.ProductId == 55);

            //No elements
            //product = await context.Products.FirstOrDefaultAsync(u => u.ProductId == 5555);

            //More than one element
            //product = await context.Products.FirstOrDefaultAsync(u => u.ProductId > 55);

            Console.WriteLine($"{product.ProductId} - {product.ProductName}");

        }

        static async Task Last()
        {
            var context = new NorthwindContext();
            Product? product;

            //Single element
            product = await context.Products.OrderByDescending(p => p.ProductName).LastAsync(u => u.ProductId == 55);

            //No elements
            //product = await context.Products.OrderByDescending(p => p.ProductName).LastAsync(u => u.ProductId == 5555);

            //More than one element
            //product = await context.Products.OrderByDescending(p => p.ProductName).LastAsync(u => u.ProductId > 55);

            Console.WriteLine($"{product.ProductId} - {product.ProductName}");

        }
        static async Task LastOrDefault()
        {
            var context = new NorthwindContext();
            Product? product;

            //Single element
            product = await context.Products.OrderBy(p => p.ProductName).LastOrDefaultAsync(u => u.ProductId == 55);

            //No elements
            //product = await context.Products.OrderBy(p => p.ProductName).LastOrDefaultAsync(u => u.ProductId == 5555);

            //More than one element
            //product = await context.Products.OrderBy(p => p.ProductName).LastOrDefaultAsync(u => u.ProductId > 55);

            Console.WriteLine($"{product.ProductId} - {product.ProductName}");

        }

        static async Task Find()
        {
            var context = new NorthwindContext();

            var product = await context.Products.FindAsync(55);
            //var product = await context.Products.FirstOrDefaultAsync(u => u.ProductId == 55);

            //With Composite Primary Key
            //var product = await context.Products.FindAsync(55, 11);

            Console.WriteLine($"{product.ProductId} - {product.ProductName}");


        }


    }
}
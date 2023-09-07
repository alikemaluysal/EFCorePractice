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
            //await Find();
            //await OtherQueryOperations();
            await CollectionOperations();

        }

        static async Task Querying()
        {
            var context = new NorthwindContext();

            //Method Syntax
            IEnumerable<Product> products = await context.Products.ToListAsync();

            //Query Syntax
            IEnumerable<Product> products2 = await (
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

        static async Task OtherQueryOperations()
        {
            var context = new NorthwindContext();

            //Count
            int count = (await context.Products.ToListAsync()).Count();
            count = await context.Products.CountAsync();

            //LongCount (COUNT_BIG)
            long longCount = await context.Products.LongCountAsync(u => u.UnitPrice > 30);

            //Any (EXISTS) 
            bool exists = await context.Products.AnyAsync(u => u.ProductName.Contains("L"));
            //bool exists = await context.Products.Where(u => u.ProductName.Contains("L")).AnyAsync();

            //Max
            var price = await context.Products.MaxAsync(u => u.UnitPrice);

            //Min
            price = await context.Products.MinAsync(u => u.UnitPrice);

            //Distinct
            var products = await context.Products.Distinct().ToListAsync();

            //All
            var allPricesBelow15000 = await context.Products.AllAsync(u => u.UnitPrice < 15000);
            var anyProductNameContainsA = await context.Products.AllAsync(u => u.ProductName.Contains("a"));

            //Sum
            var totalPrice = await context.Products.SumAsync(u => u.UnitPrice);

            //Average
            var average = await context.Products.AverageAsync(u => u.UnitPrice);

            //Like query - Contains ('%...%')
            products = await context.Products.Where(u => u.ProductName.Contains("a")).ToListAsync();

            //Like query - StartsWith ('...%')
            products = await context.Products.Where(u => u.ProductName.StartsWith("A")).ToListAsync();

            //Like query - EndsWith ('%...')
            products = await context.Products.Where(u => u.ProductName.EndsWith("a")).ToListAsync();

            Console.WriteLine();
        }

        static async Task CollectionOperations()
        {
            var context = new NorthwindContext();

            //ToList
            List<Product> productsList = await context.Products.ToListAsync();

            //ToDictionary
            Dictionary<int, string> productsDictionary = await context.Products.ToDictionaryAsync(p => p.ProductId, p => p.ProductName);

            //ToArray
            Product[] productsArray = await context.Products.ToArrayAsync();

            //Select
            //1. Specifying Columns in the Query
            List<Product> selectedProducts = await context.Products.Select(p => new Product
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice
            }).ToListAsync();

            //2. Selecting with an Anonymous Type
            var anonymousProducts = await context.Products.Select(p => new
            {
                Id = p.ProductId,
                Name = p.ProductName,
                Price = p.UnitPrice
            }).ToListAsync();

            //3. Selecting with a Different Data Type
            // Select and transform data into a different data type.
            List<ProductDetails> productDetailsWithSelect = await context.Products.Include(p => p.Category).Select(p => new ProductDetails
            {
                Id = p.ProductId,
                Name = p.ProductName,
                Price = p.UnitPrice,
                CategoryName = p.Category.CategoryName
            }).ToListAsync();

            //SelectMany (with entity relationships)
            //Combine and select associated data using SelectMany.
            List<ProductDetails> productDetailsWithSelectMany = await context.Categories.Include(p => p.Products).SelectMany(c => c.Products, (c, p) => new ProductDetails
            {
                Id = p.ProductId,
                Name = p.ProductName,
                Price = p.UnitPrice,
                CategoryName = c.CategoryName
            }).ToListAsync();

        }
    }
}
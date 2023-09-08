namespace _7_ChangeTracker;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Z_Northwind.Contexts;

using Z_Northwind.Entities;

internal class Program
{
    static async Task Main(string[] args)
    {
        var context = new NorthwindContext();

        var products = await context.Products.ToListAsync();
        products[5].UnitPrice = 123; //Modified
        products[6].ProductName = "asdasd"; //Modified
        context.Products.Remove(products[7]); //Deleted


        //ChangeTracker
        var entries = context.ChangeTracker.Entries();


        //Entries
        context.ChangeTracker.Entries().ToList().ForEach(e =>
        {

            if (e.State == EntityState.Unchanged)
            {
                //:..
            }
            else if (e.State == EntityState.Deleted)
            {
                //...
            }
            //...
        });


        //AcceptAllChanges true
        //await context.SaveChangesAsync(true);

        //AcceptAllChanges false
        //await context.SaveChangesAsync(false);
        context.ChangeTracker.AcceptAllChanges();

        //HasChanges
        var result = context.ChangeTracker.HasChanges();


        //DetectChanges
        context.ChangeTracker.DetectChanges();
        //await context.SaveChangesAsync();

        //Detached
        Product product = new Product() { ProductName = "New Product" };
        Console.WriteLine(context.Entry(product).State);

        //Unchanged
        var product2 = context.Products.FirstOrDefault(p => p.ProductId == 2);
        Console.WriteLine(context.Entry(product2).State);

        //Added
        await context.Products.AddAsync(product);
        Console.WriteLine(context.Entry(product).State);

        //Modified
        product2.ProductName = "New Product Name";
        product2.UnitPrice = 123;
        Console.WriteLine(context.Entry(product2).State);

        //Deleted
        context.Products.Remove(product2);
        Console.WriteLine(context.Entry(product2).State);


        //OriginalValues Property
        var oldProductName = context.Entry(product2).OriginalValues.GetValue<string>(nameof(Product.ProductName));
        var oldPrice = context.Entry(product2).OriginalValues.GetValue<decimal?>(nameof(Product.UnitPrice));

        //CurrentValues Property
        var productName = context.Entry(product2).CurrentValues.GetValue<string>(nameof(Product.ProductName));
        var price = context.Entry(product2).CurrentValues.GetValue<decimal?>(nameof(Product.UnitPrice));

        //GetDatabaseValues
        var productDbValues = await context.Entry(product2).GetDatabaseValuesAsync();


    }
}

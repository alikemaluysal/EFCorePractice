using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace _2_ORM_vs_SQL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await WithoutORM();
            //await WithEFCoreExtensionMethods();
            await WithEFCoreLinq();
        }

        static async Task WithoutORM()
        {
            ConfigurationManager configuration = new();
            configuration.AddJsonFile("appsettings.json");

            SqlConnection connection = new(configuration.GetConnectionString("NorthwindConStr"));
            await connection.OpenAsync();

            SqlCommand command = new(@"
                SELECT employee.FirstName, product.ProductName, COUNT(*) [Count] FROM Employees employee
                INNER JOIN Orders orders
	                ON employee.EmployeeID = orders.EmployeeID
                INNER JOIN [Order Details] orderDetail
	                ON orders.OrderID = orderDetail.OrderID
                INNER JOIN Products product
	                ON orderDetail.ProductID = product.ProductID
                GROUP By employee.FirstName, product.ProductName
                ORDER By COUNT(*) DESC
                ", connection);

            SqlDataReader dr = command.ExecuteReader();
            while (await dr.ReadAsync())
            {
                Console.WriteLine($"{dr["FirstName"]} - {dr["ProductName"]}: {dr["Count"]}");
            }

            await connection.CloseAsync();
        }

        static async Task WithEFCoreExtensionMethods()
        {
            NorthwindContext context = new NorthwindContext();

            var query = context.Employees
                .Include(employee => employee.Orders)
                    .ThenInclude(order => order.OrderDetails)
                    .ThenInclude(orderDetail => orderDetail.Product)
                .SelectMany(employee => employee.Orders, (employee, order) => new { employee.FirstName, order.OrderDetails })
                .SelectMany(data => data.OrderDetails, (data, orderDetail) => new { data.FirstName, orderDetail.Product.ProductName })
                .GroupBy(data => new
                {
                    data.ProductName,
                    data.FirstName
                })
                .Select(data => new
                {
                    data.Key.FirstName,
                    data.Key.ProductName,
                    Count = data.Count()
                });

            var data = await query.ToListAsync();

            foreach (var item in data)
            {
                Console.WriteLine($"{item.FirstName} - {item.ProductName}:  {item.Count}");
            }

        }

        static async Task WithEFCoreLinq()
        {
            NorthwindContext context = new NorthwindContext();

            var query = from employee in context.Employees
                        join order in context.Orders
                            on employee.EmployeeId equals order.EmployeeId
                        join orderDetail in context.OrderDetails
                            on order.OrderId equals orderDetail.OrderId
                        join product in context.Products
                            on orderDetail.ProductId equals product.ProductId
                        select new { employee.FirstName, product.ProductName } into result
                        group result by new { result.FirstName, result.ProductName } into groupedResult
                        select new
                        {
                            groupedResult.Key.FirstName,
                            groupedResult.Key.ProductName,
                            Count = groupedResult.Count()
                        };

            var data = await query.ToListAsync();



            foreach (var item in data)
            {
                Console.WriteLine($"{item.FirstName} - {item.ProductName}:  {item.Count}");
            }
        }

    }
}
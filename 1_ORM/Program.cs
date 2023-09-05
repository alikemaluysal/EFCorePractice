using _1_ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

#region SQL - Bad Practice

Console.WriteLine("Without ORM: ");

ConfigurationManager configuration = new();
configuration.AddJsonFile("appsettings.json");

await using SqlConnection connection = new(configuration.GetConnectionString("NorthwindConStr"));
await connection.OpenAsync();

SqlCommand command = new("Select * from Employees", connection);
SqlDataReader dr = await command.ExecuteReaderAsync();
while (await dr.ReadAsync())
{
    Console.WriteLine($"{dr["FirstName"]} {dr["LastName"]}");
}
await connection.CloseAsync();
#endregion

Console.WriteLine();

#region ORM - Best Practice

Console.WriteLine("With ORM: ");


NorthwindDbContext context = new();
var employees = await context.Employees.ToListAsync();

foreach (var employee in employees)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName}");
}

#endregion
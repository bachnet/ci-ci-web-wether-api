using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Reflection;
using web_api.Controllers;
using web_api.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace UnitTest
{
    public class DemoTest
    {
        [Fact]
        public void Test1()
        {

            Assert.True(1 == 1);
        }
        [Fact]
        public async Task CustomerIntegrationTest()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
          
            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
            optionsBuilder.UseMySQL(configuration["ConnectionStrings:DefaultConnection"]);

            var context = new CustomerContext();

            // Just to make sur: Delete all existing customers in the DB
            context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
            await context.SaveChangesAsync();

            // Create controller

            var controller = new CustomerController(context);
            //add Customer
            await controller.Create(new Customer() { Name = "FooBar" });

            // Check does GetAll return the added customer?
            var result = await controller.GetAll();
            Assert.Single(result);
            Assert.Equal("FooBar", result[0].Name);
        }
    }
}
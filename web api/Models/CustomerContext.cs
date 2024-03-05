using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Security.Policy;

namespace web_api.Models
{
    public class CustomerContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=1234$;database=cicddemo;");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
 
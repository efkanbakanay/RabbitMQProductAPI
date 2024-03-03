using Microsoft.EntityFrameworkCore;
using RabbitMQProductAPI.Publisher.Models;

namespace RabbitMQProductAPI.Publisher.Context
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configoration;
        public DbContextClass(IConfiguration configoration)
        {
            Configoration = configoration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configoration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Product> Products { get; set; }
    }
}

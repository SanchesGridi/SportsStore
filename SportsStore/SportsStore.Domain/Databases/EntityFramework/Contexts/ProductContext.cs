using System.Data.Entity;
using SportsStore.Domain.Databases.EntityFramework.Entities;

namespace SportsStore.Domain.Databases.EntityFramework.Contexts
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}

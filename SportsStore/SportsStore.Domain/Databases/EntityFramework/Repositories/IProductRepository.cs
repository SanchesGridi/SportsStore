using System.Collections.Generic;
using System.Threading.Tasks;
using SportsStore.Domain.Databases.EntityFramework.Entities;

namespace SportsStore.Domain.Databases.EntityFramework.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        Product GetProductById(int id);
        Task SaveProductAsync(Product product);
        Task<Product> DeleteProductAsync(int id);
    }
}

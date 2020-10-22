using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Domain.Databases.EntityFramework.Contexts;
using SportsStore.Domain.Databases.EntityFramework.Entities;

namespace SportsStore.Domain.Databases.EntityFramework.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _productContext;

        public IEnumerable<Product> Products { get => _productContext.Products; }

        public ProductRepository()
        {
            _productContext = new ProductContext();
        }

        public Product GetProductById(int id)
        {
            return _productContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public async Task SaveProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product equals null!");
            }
            else
            {
                if (product.Id == 0)
                {
                    _productContext.Products.Add(product);
                }
                else
                {
                    var entryProduct = this.GetProductById(product.Id);

                    if (entryProduct != null)
                    {
                        entryProduct.Name = product.Name;
                        entryProduct.Description = product.Description;
                        entryProduct.Price = product.Price;
                        entryProduct.Category = product.Category;

                        entryProduct.ImageData = product.ImageData;
                        entryProduct.ImageMimeType = product.ImageMimeType;
                    }
                }

                await _productContext.SaveChangesAsync();
            }
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var entryProduct = this.GetProductById(id);

            if (entryProduct != null)
            {
                _productContext.Products.Remove(entryProduct);

                await _productContext.SaveChangesAsync();
            }

            return entryProduct;
        }
    }
}

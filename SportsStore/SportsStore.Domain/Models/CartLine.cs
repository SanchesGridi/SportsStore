using SportsStore.Domain.Databases.EntityFramework.Entities;

namespace SportsStore.Domain.Models
{
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal ComputeCartLineValue()
        {
            return Product.Price * Quantity;
        }
    }
}

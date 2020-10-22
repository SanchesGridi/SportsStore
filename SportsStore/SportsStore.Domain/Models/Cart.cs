using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Databases.EntityFramework.Entities;

namespace SportsStore.Domain.Models
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection;

        public Cart()
        {
            _lineCollection = new List<CartLine>();
        }

        public void AddItem(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var line = _lineCollection.FirstOrDefault(p => p.Product.Id == product.Id);

            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            _lineCollection.RemoveAll(cl => cl.Product.Id == product.Id);
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }

        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(cl => cl.ComputeCartLineValue());
        }

        public IEnumerable<CartLine> GetCartLines()
        {
            return _lineCollection;
        }

        public int GetCartLinesCount()
        {
            return _lineCollection.Count();
        }

        public void RemovePositionFromCartLine(int productId)
        {
            var line = _lineCollection.FirstOrDefault(cl => cl.Product.Id == productId);

            const int position = 1;
            line.Quantity -= position;

            if (line.Quantity == 0)
            {
                _lineCollection.Remove(line);
            }
        }

        public string GetItemsInfo()
        {
            var builder = new StringBuilder();
            var quantity = _lineCollection.Sum(x => x.Quantity);

            if (quantity < 0)
            {
                throw new ApplicationException("Cart data error: [Quantity < 0]");
            }
            if (quantity == 0)
            {
                builder.Append("empty");
            }
            else
            {
                var info = quantity == 1 ? "Item" : "Items";
                builder.Append($"{quantity} - {info}");
            }

            builder.Append($", {this.ComputeTotalValue():c}");

            return builder.ToString();
        }
    }
}

using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Extensions;
using SportsStore.Domain.Models;

namespace SportsStore.Domain.Processors
{
    public class OrderMessageBuilder
    {
        private const string OrderTitle = "New order submitted!";
        private const string Separator = "--->--->--->";

        private readonly StringBuilder _stringBuilder;

        public OrderMessageBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        public string GetOrderTitle()
        {
            return OrderTitle;
        }

        public async Task<string> BuildOrderMessageAsync(Cart cart, ShippingDetails shippingDetails)
        {
            return await Task.Run(() =>
            {
                _stringBuilder.AppendLine("A new order  has been submitted")
                    .AppendLine(Separator)
                    .AppendLine("Items:");

                foreach (var line in cart.GetCartLines())
                {
                    var subtotal = line.ComputeCartLineValue();

                    _stringBuilder.AppendLine($"{line.Quantity} * {line.Product.Name} = subtotal: {subtotal:c}");
                }

                _stringBuilder.AppendLine($"Total order value: {cart.ComputeTotalValue():c}")
                    .AppendLine(Separator)
                    .AppendLine("Ship to:")
                    .AppendLine($"client: {shippingDetails.Name}")
                    .AppendLine($"first address line: {shippingDetails.Line1}")
                    .AppendExistingLinesWithStartAdditions(additions: new[] { "second address line: ", "third address line: " }, lines: new[] { shippingDetails.Line2, shippingDetails.Line3 })
                    .AppendLine($"city: {shippingDetails.City}")
                    .AppendLine($"state: {shippingDetails.State}")
                    .AppendLine($"zip: {shippingDetails.Zip}")
                    .AppendLine(Separator)
                    .AppendLine($"Gift wrap: {(shippingDetails.GiftWrap ? "Yes" : "No")}");

                var orderMessage = _stringBuilder.ToString();

                _stringBuilder.Clear();

                return orderMessage;
            });
        }
    }
}

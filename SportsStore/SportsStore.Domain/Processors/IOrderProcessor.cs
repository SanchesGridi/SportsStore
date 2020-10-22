using System.Threading.Tasks;
using SportsStore.Domain.Models;

namespace SportsStore.Domain.Processors
{
    public interface IOrderProcessor
    {
        Task ProcessOrderAsync(Cart cart, ShippingDetails shippingDetails);
    }
}

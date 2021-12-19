
using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int customerId);
    }
}

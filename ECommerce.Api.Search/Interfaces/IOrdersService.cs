using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrdersService
    {
       Task<IEnumerable<Order>> GetOrdersAsync(int customerId);
    }
}

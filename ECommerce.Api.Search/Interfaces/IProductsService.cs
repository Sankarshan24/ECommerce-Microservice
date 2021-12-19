using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductsService
    {
       Task<IEnumerable<Product>> GetProductsAsync();
    }
}

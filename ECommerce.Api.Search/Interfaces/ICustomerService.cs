namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<dynamic> GetCustomerAsync(int id); 
    }
}

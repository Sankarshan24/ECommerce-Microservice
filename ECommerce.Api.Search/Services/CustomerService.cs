using ECommerce.Api.Search.Interfaces;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<CustomerService> _logger { get; }


        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }


        public async Task<dynamic> GetCustomerAsync(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<dynamic>(content, options);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return ex.Message;
            }
        }
    }
}
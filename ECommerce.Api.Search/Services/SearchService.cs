using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        public readonly IProductsService _productsService;
        public ICustomerService _customerService { get; }

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomerService customerService)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customerService = customerService;
        }


        public async Task<dynamic> Searchsync(int customerId)
        {
            var orders = await _ordersService.GetOrdersAsync(customerId);
            var products = await _productsService.GetProductsAsync();
            var customers = await _customerService.GetCustomerAsync(customerId);

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = products.FirstOrDefault(obj => obj.Id == item.ProductId)?.Name;
                    }
                }
            }
            return new
            {
                customers = customers,
                orders = orders,
                products = products
            };
        }
    }
}

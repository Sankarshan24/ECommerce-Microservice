using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrderProvider
    {
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Orders.Any())
            {
                _dbContext.Orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    Items = new List<OrderItem> { new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 10 } },
                    OrderedDate = DateTime.Now,
                    Total = 10
                });
                _dbContext.Orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    Items = new List<OrderItem> { new OrderItem { Id = 2, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 100 } },
                    OrderedDate = DateTime.Now,
                    Total = 10
                });
                _dbContext.Orders.Add(new Order()
                {
                    Id = 3,
                    CustomerId = 3,
                    Items = new List<OrderItem> { new OrderItem { Id = 3, OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 1000 } },
                    OrderedDate = DateTime.Now,
                    Total = 1000
                });
                _dbContext.Orders.Add(new Order()
                {
                    Id = 4,
                    CustomerId = 4,
                    Items = new List<OrderItem> { new OrderItem { Id = 4, OrderId = 4, ProductId = 4, Quantity = 1, UnitPrice = 10000 } },
                    OrderedDate = DateTime.Now,
                    Total = 1000
                });
                _dbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<Models.Order>> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _dbContext.Orders.Where(obj => obj.CustomerId == customerId).ToListAsync(); ;
                var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

        }

        public async Task<Models.Order> GetOrderAsync(int id)
        {
            try
            {
                return _mapper.Map<Db.Order, Models.Order>(await _dbContext.Orders.FirstOrDefaultAsync(obj => obj.Id == id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}

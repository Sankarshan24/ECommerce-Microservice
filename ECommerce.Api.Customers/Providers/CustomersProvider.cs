using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomerProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly ILogger<CustomersProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.Add(new Customer() { Id = 1, Name = "A", Address = "1st steet" });
                _dbContext.Customers.Add(new Customer() { Id = 2, Name = "B", Address = "2nd steet" });
                _dbContext.Customers.Add(new Customer() { Id = 3, Name = "C", Address = "3rd steet" });
                _dbContext.Customers.Add(new Customer() { Id = 4, Name = "D", Address = "4th steet" });
                _dbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<Models.Customer>> GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();
                var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Models.Customer> GetCustomerAsync(int id)
        {
            try
            {
                return _mapper.Map<Db.Customer, Models.Customer>(await _dbContext.Customers.FirstOrDefaultAsync(obj => obj.Id == id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}

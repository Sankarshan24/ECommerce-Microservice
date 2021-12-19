using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new Product() { Id = 1, Name = "Keyboard", Price = 10, Inventory = 100 });
                _dbContext.Products.Add(new Product() { Id = 2, Name = "Mouse", Price = 100, Inventory = 100 });
                _dbContext.Products.Add(new Product() { Id = 3, Name = "Monitor", Price = 1000, Inventory = 100 });
                _dbContext.Products.Add(new Product() { Id = 4, Name = "Router", Price = 10000, Inventory = 100 });
                _dbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<Models.Product>> GetProductsAsync()
        { 
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

        }

        public async Task<Models.Product> GetProductAsync(int id)
        {
            try
            {
                return _mapper.Map<Db.Product, Models.Product>(await _dbContext.Products.FirstOrDefaultAsync(obj => obj.Id == id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}

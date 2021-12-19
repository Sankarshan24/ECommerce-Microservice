
namespace ECommerce.Api.Products.Profiles
{
    public class ProductsProfiles : AutoMapper.Profile
    {
        public ProductsProfiles()
        {
            CreateMap<Db.Product, Models.Product>();
        }
    }
}


namespace ECommerce.Api.Customers.Profiles
{
    public class CustomersProfiles : AutoMapper.Profile
    {
        public CustomersProfiles()
        {
            CreateMap<Db.Customer, Models.Customer>();
        }
    }
}

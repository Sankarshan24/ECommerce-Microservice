namespace ECommerce.Api.Search.Interfaces
{
    public interface ISearchService
    {
       Task<dynamic> Searchsync(int customerId);
    }
}

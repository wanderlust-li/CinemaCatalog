using CinemaCatalog.Web.Services.IServices;

namespace CinemaCatalog.Web.Services;

public class CategoryService : BaseService, ICategoryService
{
    public CategoryService(IHttpClientFactory httpClient) : base(httpClient)
    {
    }

    public Task<T> GetAllAsync<T>()
    {
        throw new NotImplementedException();
    }
}
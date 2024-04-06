using CinemaCatalog.Web.Models;
using CinemaCatalog.Web.Services.IServices;

namespace CinemaCatalog.Web.Services;

public class CategoryService : BaseService, ICategoryService
{
    private readonly IHttpClientFactory _clientFactory;
    private string movieCatalogUrl;
    
    public CategoryService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        movieCatalogUrl = configuration.GetValue<string>("ServiceUrls:CinemaCatalogAPI");
    }

    public Task<T> GetAllAsync<T>()
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = ApiType.GET,
            Url = movieCatalogUrl + "/Category/get-all-categories"
        });
    }
}
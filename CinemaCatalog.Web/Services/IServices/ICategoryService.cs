namespace CinemaCatalog.Web.Services.IServices;

public interface ICategoryService
{
    Task<T> GetAllAsync<T>();
}
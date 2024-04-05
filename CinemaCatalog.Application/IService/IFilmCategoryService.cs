using CinemaCatalog.Domain;

namespace CinemaCatalog.Application.IService;

public interface IFilmCategoryService
{
    Task<List<FilmCategory>> GetFilmCategory();
}
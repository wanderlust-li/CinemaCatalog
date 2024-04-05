using CinemaCatalog.Application.DTO.FilmCategoryDTO;
using CinemaCatalog.Domain;

namespace CinemaCatalog.Application.IService;

public interface IFilmCategoryService
{
    Task<List<FilmCategoryDTO>> GetFilmCategory();
}
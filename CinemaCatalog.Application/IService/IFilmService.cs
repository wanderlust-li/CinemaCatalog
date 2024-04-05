using CinemaCatalog.Application.DTO.FilmCategoryDTO;
using CinemaCatalog.Application.DTO.FilmDTO;
using CinemaCatalog.Domain;

namespace CinemaCatalog.Application.IService;

public interface IFilmService
{
    Task<List<FilmDTO>> GetFilmsWithCategories();
    
    Task<FilmDTO> AddFilm(CreateFilmDTO createFilmDTO);

    Task<FilmDTO> GetFilmById(int Id);

    Task<FilmDTO> RemoveFilmById(int Id);

    Task<FilmDTO> UpdateFilm(UpdateFilmDTO updateFilmDto);
}
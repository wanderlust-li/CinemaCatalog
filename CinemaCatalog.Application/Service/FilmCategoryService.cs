using AutoMapper;
using CinemaCatalog.Application.DTO.FilmCategoryDTO;
using CinemaCatalog.Application.DTO.FilmDTO;
using CinemaCatalog.Application.IRepository;
using CinemaCatalog.Application.IService;
using CinemaCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace CinemaCatalog.Application.Service;

public class FilmCategoryService : IFilmCategoryService
{
    private readonly IRepository<FilmCategory> _filmCategoryRepository;
    private readonly IMapper _mapper;
    
    public FilmCategoryService(IRepository<FilmCategory> filmCategoryRepository, IMapper mapper)
    {
        _filmCategoryRepository = filmCategoryRepository;
        _mapper = mapper;
    }

    public async Task<List<FilmCategoryDTO>> GetFilmCategory()
    {
        var query = _filmCategoryRepository.Query()
            .Include(filmCategory => filmCategory.Film)
            .Include(filmCategory => filmCategory.Category);

        var filmCategories = await query.ToListAsync();

        return _mapper.Map<List<FilmCategoryDTO>>(filmCategories);
    }

}
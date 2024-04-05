using AutoMapper;
using CinemaCatalog.Application.DTO.FilmCategoryDTO;
using CinemaCatalog.Application.DTO.FilmDTO;
using CinemaCatalog.Application.Exceptions;
using CinemaCatalog.Application.IRepository;
using CinemaCatalog.Application.IService;
using CinemaCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace CinemaCatalog.Application.Service;

public class FilmService : IFilmService
{
    private readonly IRepository<FilmCategory> _filmCategoryRepository;
    private readonly IRepository<Film> _filmRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    // Внедрення залежностей IRepository<FilmCategory> та IMapper
    public FilmService(IRepository<FilmCategory> filmCategoryRepository, IMapper mapper,
        IRepository<Film> filmRepository, IRepository<Category> categoryRepository)
    {
        _filmCategoryRepository = filmCategoryRepository;
        _filmRepository = filmRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<FilmDTO>> GetFilmsWithCategories()
    {
        var filmCategories = await _filmCategoryRepository.Query()
            .Include(filmCategory => filmCategory.Film)
            .Include(filmCategory => filmCategory.Category)
            .GroupBy(fc => fc.Film)
            .Select(group => new FilmDTO
            {
                Id = group.Key.Id,
                Name = group.Key.Name,
                Director = group.Key.Director,
                Release = group.Key.Release,
                Categories = group.Select(fc => fc.Category.Name).ToList()
            })
            .ToListAsync();
        
        return filmCategories;
    }

    public async Task<FilmDTO> AddFilm(CreateFilmDTO createFilmDTO)
    {
        var film = new Film
        {
            Name = createFilmDTO.Name,
            Director = createFilmDTO.Director,
            Release = createFilmDTO.Release
        };
        
        await _filmRepository.AddAsync(film);
        await _filmRepository.SaveChangesAsync();
        
        foreach (var categoryId in createFilmDTO.CategoryIds)
        {
            var category = await _categoryRepository.FindByIdAsync(categoryId);
            if (category != null)
            {
                var filmCategory = new FilmCategory
                {
                    Film = film,
                    Category = category
                };

                await _filmCategoryRepository.AddAsync(filmCategory);
            }
        }

        await _filmCategoryRepository.SaveChangesAsync();
        
        var filmDTO = _mapper.Map<FilmDTO>(film);
        filmDTO.Categories = createFilmDTO.CategoryIds
            .Select(id => _categoryRepository.FindByIdAsync(id).Result.Name).ToList();

        return filmDTO;
    }

    public async Task<FilmDTO> GetFilmById(int id)
    {
        var filmCategories = await _filmCategoryRepository.Query()
            .Include(fc => fc.Film)
            .Include(fc => fc.Category)
            .Where(fc => fc.FilmId == id)
            .ToListAsync();

        if (!filmCategories.Any())
        {
            throw new NotFoundException("Film not found.");
        }

        var filmDto = filmCategories
            .GroupBy(fc => fc.Film)
            .Select(group => new FilmDTO
            {
                Id = group.Key.Id,
                Name = group.Key.Name,
                Director = group.Key.Director,
                Release = group.Key.Release,
                Categories = group.Select(fc => fc.Category.Name).Distinct().ToList()
            })
            .FirstOrDefault();

        return filmDto;
    }

    public async Task<FilmDTO> RemoveFilmById(int id)
    {
        var filmCategories = await _filmCategoryRepository.Query().Where(fc => fc.FilmId == id).ToListAsync();
        foreach (var filmCategory in filmCategories)
        {
            await _filmCategoryRepository.RemoveByIdAsync(filmCategory.Id);
        }
        
        var film = await _filmRepository.RemoveByIdAsync(id);
        if (film == null)
        {
            throw new NotFoundException($"Film with ID {id} not found.");
        }
        await _filmRepository.SaveChangesAsync();
        
        var filmDTO = _mapper.Map<FilmDTO>(film);
        return filmDTO;
    }

    public async Task<FilmDTO> UpdateFilm(UpdateFilmDTO updateFilmDto)
    {
        // Знайдіть фільм, який потрібно оновити
        var film = await _filmRepository.FindByIdAsync(updateFilmDto.Id);
        if (film == null)
        {
            throw new NotFoundException($"Film with ID {updateFilmDto.Id} not found.");
        }

        // Оновіть поля фільму
        film.Name = updateFilmDto.Name;
        film.Director = updateFilmDto.Director;
        film.Release = updateFilmDto.Release;
        await _filmRepository.SaveChangesAsync();

        // Видаліть існуючі зв'язки фільму з категоріями
        var existingFilmCategories = await _filmCategoryRepository.Query()
            .Where(fc => fc.FilmId == film.Id)
            .ToListAsync();
        foreach (var fc in existingFilmCategories)
        {
            _filmCategoryRepository.RemoveByIdAsync(fc.Id);
        }
        await _filmCategoryRepository.SaveChangesAsync();

        // Додайте нові зв'язки з категоріями
        foreach (var categoryId in updateFilmDto.CategoryIds)
        {
            var newFilmCategory = new FilmCategory
            {
                FilmId = film.Id,
                CategoryId = categoryId
            };
            await _filmCategoryRepository.AddAsync(newFilmCategory);
        }
        await _filmCategoryRepository.SaveChangesAsync();

        // Підготуйте і поверніть оновлений DTO
        var updatedFilmDTO = _mapper.Map<FilmDTO>(film);
        updatedFilmDTO.Categories = await _categoryRepository.Query()
            .Where(c => updateFilmDto.CategoryIds.Contains(c.Id))
            .Select(c => c.Name)
            .ToListAsync();

        return updatedFilmDTO;
    }

}
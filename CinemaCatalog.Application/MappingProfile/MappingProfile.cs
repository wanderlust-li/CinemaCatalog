using AutoMapper;
using CinemaCatalog.Application.DTO.CategoryDTO;
using CinemaCatalog.Application.DTO.FilmCategoryDTO;
using CinemaCatalog.Application.DTO.FilmDTO;
using CinemaCatalog.Domain;

namespace CinemaCatalog.Application.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Film, FilmDTO>();
        CreateMap<Category, CategoryDTO>();
        CreateMap<Category, CategoryWithDetailsDTO>();
        CreateMap<FilmCategory, FilmCategoryDTO>()
            .ForMember(dest => dest.Film, opt => opt.MapFrom(src => src.Film))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
    }
}
using CinemaCatalog.Application.DTO.CategoryDTO;

namespace CinemaCatalog.Application.IService;

public interface ICategoryService
{
    Task<IEnumerable<CategoryWithDetailsDTO>> GetAllCategoriesAsync();

    Task<CategoryWithDetailsDTO> GetCategoryById(int Id);

    Task<CategoryWithDetailsDTO> CreateCategory(CreateCategoryDTO createCategoryDto);

    Task<CategoryWithDetailsDTO> RemoveCategory(int Id);

    Task<CategoryWithDetailsDTO> UpdateCategory(UpdateCategoryDTO updateCategoryDto);
}
using AutoMapper;
using CinemaCatalog.Application.DTO.CategoryDTO;
using CinemaCatalog.Application.Exceptions;
using CinemaCatalog.Application.IRepository;
using CinemaCatalog.Application.IService;
using CinemaCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace CinemaCatalog.Application.Service;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CategoryWithDetailsDTO>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoryDTOs = _mapper.Map<IEnumerable<CategoryWithDetailsDTO>>(categories);
        return categoryDTOs;
    }

    public async Task<CategoryWithDetailsDTO> GetCategoryById(int Id)
    {
        var category = await _categoryRepository.FindByIdAsync(Id);
        if (category == null)
            throw new NotFoundException("category is null");

        return _mapper.Map<CategoryWithDetailsDTO>(category);
    }

    public async Task<CategoryWithDetailsDTO> CreateCategory(CreateCategoryDTO createCategoryDto)
    {
        var existingCategory = await _categoryRepository.Query()
            .FirstOrDefaultAsync(c => c.Name == createCategoryDto.Name);
        if (existingCategory != null)
        {
            throw new BadRequestException("A category with the same name already exists.");
        }
        Category parentCategory = null;
        if (createCategoryDto.ParentCategoryId.HasValue)
        {
            parentCategory = await _categoryRepository.FindByIdAsync(createCategoryDto.ParentCategoryId.Value);
            if (parentCategory == null)
            {
                throw new NotFoundException($"Parent category with ID {createCategoryDto.ParentCategoryId} not found.");
            }
        }
        
        var newCategory = new Category
        {
            Name = createCategoryDto.Name,
            ParentCategoryId = createCategoryDto.ParentCategoryId,
            ParentCategory = parentCategory
        };

        await _categoryRepository.AddAsync(newCategory);
        await _categoryRepository.SaveChangesAsync();
        
        return _mapper.Map<CategoryWithDetailsDTO>(newCategory);
    }

    public async Task<CategoryWithDetailsDTO> RemoveCategory(int id)
    {
        var categoryToRemove = await _categoryRepository.FindByIdAsync(id);
        if (categoryToRemove == null)
        {
            throw new NotFoundException($"Category with ID {id} not found.");
        }

        var childCategories = await _categoryRepository.Query().Where(c => c.ParentCategoryId == id).ToListAsync();
        foreach (var childCategory in childCategories)
        {
            childCategory.ParentCategoryId = null;
        }

        await _categoryRepository.RemoveByIdAsync(categoryToRemove.Id);
        await _categoryRepository.SaveChangesAsync();

        return _mapper.Map<CategoryWithDetailsDTO>(categoryToRemove);
    }

    public async Task<CategoryWithDetailsDTO> UpdateCategory(UpdateCategoryDTO updateCategoryDto)
    {
        var categoryToUpdate = await _categoryRepository.FindByIdAsync(updateCategoryDto.Id);
        if (categoryToUpdate == null)
        {
            throw new NotFoundException($"Category with ID {updateCategoryDto.Id} not found.");
        }

        var existingCategory = await _categoryRepository.Query()
            .FirstOrDefaultAsync(c => c.Name == updateCategoryDto.Name && c.Id != updateCategoryDto.Id);
        if (existingCategory != null)
        {
            throw new BadRequestException("A category with the same name already exists.");
        }
        
        categoryToUpdate.Name = updateCategoryDto.Name;
        
        if (updateCategoryDto.ParentCategoryId.HasValue)
        {
            var parentCategory = await _categoryRepository.FindByIdAsync(updateCategoryDto.ParentCategoryId.Value);
            if (parentCategory == null)
            {
                throw new NotFoundException($"Parent category with ID {updateCategoryDto.ParentCategoryId} not found.");
            }
            categoryToUpdate.ParentCategoryId = updateCategoryDto.ParentCategoryId.Value;
            categoryToUpdate.ParentCategory = parentCategory;
        }
        else
        {
            categoryToUpdate.ParentCategoryId = null;
            categoryToUpdate.ParentCategory = null;
        }
        
        await _categoryRepository.SaveChangesAsync();
        
        return _mapper.Map<CategoryWithDetailsDTO>(categoryToUpdate);
    }

}
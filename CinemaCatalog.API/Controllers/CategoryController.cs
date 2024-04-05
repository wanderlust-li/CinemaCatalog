using CinemaCatalog.Application.DTO.CategoryDTO;
using CinemaCatalog.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCatalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet("get-all-categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _categoryService.GetAllCategoriesAsync());
    }

    [HttpGet("get-category-by-id")]
    public async Task<IActionResult> GetGategory(int Id)
    {
        return Ok(await _categoryService.GetCategoryById(Id));
    }

    [HttpPost("create-category")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO createCategoryDto)
    {
        return Ok(await _categoryService.CreateCategory(createCategoryDto));
    }

    [HttpDelete("delete-category")]
    public async Task<IActionResult> DeleteCategory(int Id)
    {
        return Ok(await _categoryService.RemoveCategory(Id));
    }

    [HttpPut("update-category")]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDto)
    {
        return Ok(await _categoryService.UpdateCategory(updateCategoryDto));
    }
}
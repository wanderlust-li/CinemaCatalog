using CinemaCatalog.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCatalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmCategoryController : ControllerBase
{
    private readonly IFilmCategoryService _filmCategoryService;

    public FilmCategoryController(IFilmCategoryService filmCategoryService)
    {
        _filmCategoryService = filmCategoryService;
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllFilmCategories()
    {
        try
        {
            var filmCategories = await _filmCategoryService.GetFilmCategory();
            return Ok(filmCategories);
        }
        catch (Exception ex)
        {
            // Обробка помилок
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
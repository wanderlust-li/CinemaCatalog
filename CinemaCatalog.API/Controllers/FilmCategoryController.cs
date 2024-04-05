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
        var filmCategories = await _filmCategoryService.GetFilmCategory();
        return Ok(filmCategories);
    }
}
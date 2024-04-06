using CinemaCatalog.Web.Models;
using CinemaCatalog.Web.Models.Category;
using CinemaCatalog.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CinemaCatalog.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> IndexCategory()
    {
        List<CategoryWithDetailsDTO> categoryWithDetailsDtos = new();
        
        var response = await _categoryService.GetAllAsync<APIResponse>();
        
        if (response != null && response.IsSuccess)
        {
            categoryWithDetailsDtos = JsonConvert.DeserializeObject<List<CategoryWithDetailsDTO>>(Convert.ToString(response.Result));
        }

        return View(categoryWithDetailsDtos);
    }

    // GET
    /*public IActionResult Index()
    {
        return View();
    }*/
}
using CinemaCatalog.Web.Models;
using CinemaCatalog.Web.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CinemaCatalog.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly HttpClient _httpClient;

    public CategoryController(ILogger<CategoryController> logger,
        HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> IndexCategory()
    {
        List<CategoryWithDetailsDTO> categoryWithDetailsDtos = new();

        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5042/Film/get-films-with-details-categories");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                categoryWithDetailsDtos = JsonConvert.DeserializeObject<List<CategoryWithDetailsDTO>>(responseString);
            }
            else
            {
                _logger.LogError($"Failed to fetch data: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception occurred while fetching data: {ex.Message}");
        }
        
        return View(categoryWithDetailsDtos);
    }

}
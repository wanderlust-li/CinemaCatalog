using CinemaCatalog.Web.Models;
using CinemaCatalog.Web.Models.Film;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace CinemaCatalog.Web.Controllers;

public class FilmController : Controller
{
    private readonly ILogger<FilmController> _logger;
    private readonly HttpClient _httpClient;

    public FilmController(ILogger<FilmController> logger, HttpClient httpClient)
    {
        _logger = logger;
        // Use HttpClientFactory to manage HttpClient lifetimes efficiently
        _httpClient = httpClient;
    }
    
    public async Task<IActionResult> IndexFilm()
    {
        List<FilmDTO> filmsWithDetailsDtos = new();
    
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5042/Film/get-films-with-details");
            
            _logger.LogInformation($"Response status code: {response.StatusCode}");
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                filmsWithDetailsDtos = JsonConvert.DeserializeObject<List<FilmDTO>>(responseContent);
            }
            else
            {
                // Log the error message or handle it as needed
                _logger.LogError($"API call failed: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception occurred: {ex.Message}");
        }
        
        return View(filmsWithDetailsDtos);
    }
}
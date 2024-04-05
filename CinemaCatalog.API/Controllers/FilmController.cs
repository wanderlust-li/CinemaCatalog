using CinemaCatalog.Application.DTO.FilmDTO;
using CinemaCatalog.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCatalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private readonly IFilmService _filmService;

    public FilmController(IFilmService filmService)
    {
        _filmService = filmService;
    }

    [HttpGet("get-films-with-details-categories")]
    public async Task<IActionResult> GetFilmsWithDetailsCategories()
    {
        var films = await _filmService.GetFilmsWithCategories();
        return Ok(films);
    }

    [HttpPost("add-film")]
    public async Task<IActionResult> AddFilm([FromBody] CreateFilmDTO createFilmDTO)
    {
        var filmDTO = await _filmService.AddFilm(createFilmDTO);
        return Ok(filmDTO);
    }

    [HttpGet("get-film")]
    public async Task<IActionResult> GetFilm(int Id)
    {
        return Ok(await _filmService.GetFilmById(Id));
    }
    
    [HttpDelete("remove-film")]
    public async Task<IActionResult> RemoveFilm(int Id)
    {
        return Ok(await _filmService.RemoveFilmById(Id));
    }

    [HttpPut("update-film")]
    public async Task<IActionResult> UpdateFilm(UpdateFilmDTO updateFilmDto)
    {
        return Ok(await _filmService.UpdateFilm(updateFilmDto));
    }
}
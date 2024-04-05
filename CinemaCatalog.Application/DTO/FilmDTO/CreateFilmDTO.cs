namespace CinemaCatalog.Application.DTO.FilmDTO;

public class CreateFilmDTO
{
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime Release { get; set; }
    public List<int> CategoryIds { get; set; } = new List<int>();
}
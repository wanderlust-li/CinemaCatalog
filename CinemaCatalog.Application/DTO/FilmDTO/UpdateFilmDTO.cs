namespace CinemaCatalog.Application.DTO.FilmDTO;

public class UpdateFilmDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime Release { get; set; }
    public List<int> CategoryIds { get; set; } = new List<int>();
}
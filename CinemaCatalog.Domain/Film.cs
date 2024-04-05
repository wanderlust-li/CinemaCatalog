namespace CinemaCatalog.Domain;

public class Film
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime Release { get; set; }
    
    public ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();
}
namespace CinemaCatalog.Domain;

public class Film
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime Release { get; set; }
    
    public virtual ICollection<FilmCategory> FilmCategories { get; set; }

    public Film()
    {
        FilmCategories = new HashSet<FilmCategory>();
    }
}
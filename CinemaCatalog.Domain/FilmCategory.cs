namespace CinemaCatalog.Domain;

public class FilmCategory
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int CategoryId { get; set; }
    
    public virtual Film Film { get; set; }
    public virtual Category Category { get; set; }
}
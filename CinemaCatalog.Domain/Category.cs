namespace CinemaCatalog.Domain;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    
    public virtual Category ParentCategory { get; set; }
    public virtual ICollection<Category> SubCategories { get; set; }
    public virtual ICollection<FilmCategory> FilmCategories { get; set; }

    public Category()
    {
        SubCategories = new HashSet<Category>();
        FilmCategories = new HashSet<FilmCategory>();
    }
}
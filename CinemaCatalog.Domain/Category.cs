namespace CinemaCatalog.Domain;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public ICollection<FilmCategory> FilmCategories { get; set; } = new List<FilmCategory>();
}
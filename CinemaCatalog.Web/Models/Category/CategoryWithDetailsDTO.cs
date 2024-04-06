namespace CinemaCatalog.Web.Models.Category;

public class CategoryWithDetailsDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}
namespace CinemaCatalog.Application.DTO.CategoryDTO;

public class UpdateCategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}
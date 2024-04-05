namespace CinemaCatalog.Application.DTO.CategoryDTO;

public class CreateCategoryDTO
{
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}
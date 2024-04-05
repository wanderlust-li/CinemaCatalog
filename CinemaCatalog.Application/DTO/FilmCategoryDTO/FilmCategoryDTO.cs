namespace CinemaCatalog.Application.DTO.FilmCategoryDTO;

public class FilmCategoryDTO
{
    public int FilmId { get; set; }
    public int CategoryId { get; set; }
    public FilmDTO.FilmDTO Film { get; set; }
    public CategoryDTO.CategoryDTO Category { get; set; }
}
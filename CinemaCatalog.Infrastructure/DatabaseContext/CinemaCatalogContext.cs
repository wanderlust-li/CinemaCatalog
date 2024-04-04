using CinemaCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace CinemaCatalog.Infrastructure.DatabaseContext;

public class CinemaCatalogContext : DbContext
{
    public CinemaCatalogContext(DbContextOptions<CinemaCatalogContext> options) : base(options)
    {
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<FilmCategory> FilmCategories { get; set; }
    
    
}
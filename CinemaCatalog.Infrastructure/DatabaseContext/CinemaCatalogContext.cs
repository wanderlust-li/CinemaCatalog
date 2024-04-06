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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FilmCategory>()
            .HasOne(fc => fc.Film)
            .WithMany(f => f.FilmCategories)
            .HasForeignKey(fc => fc.FilmId);

        modelBuilder.Entity<FilmCategory>()
            .HasOne(fc => fc.Category)
            .WithMany(c => c.FilmCategories)
            .HasForeignKey(fc => fc.CategoryId);
        
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .IsRequired(false);
        
        // add data
        modelBuilder.Entity<Film>().HasData(
            new Film { Id = 1, Name = "Inception", Director = "Christopher Nolan", Release = new DateTime(2010, 7, 16) },
            new Film { Id = 2, Name = "Interstellar", Director = "Christopher Nolan", Release = new DateTime(2014, 11, 7) }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Science Fiction", ParentCategoryId = null }, 
            new Category { Id = 2, Name = "Adventure", ParentCategoryId = null }, 
            new Category { Id = 3, Name = "Drama", ParentCategoryId = 1 }
        );


        modelBuilder.Entity<FilmCategory>().HasData(
            new FilmCategory { Id = 1, FilmId = 1, CategoryId = 1 },
            new FilmCategory { Id = 2, FilmId = 1, CategoryId = 3 },
            new FilmCategory { Id = 3, FilmId = 2, CategoryId = 1 },
            new FilmCategory { Id = 4, FilmId = 2, CategoryId = 2 }
        );
    }
}
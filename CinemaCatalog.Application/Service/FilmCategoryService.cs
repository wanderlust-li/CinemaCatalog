using CinemaCatalog.Application.IService;
using CinemaCatalog.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CinemaCatalog.Application.Service;

public class FilmCategoryService : IFilmCategoryService
{
    private readonly string _connectionString;

    public FilmCategoryService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");

    }

    public async Task<List<FilmCategory>> GetFilmCategory()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var result = await connection.QueryAsync<FilmCategory, Film, Category, FilmCategory>(
                @"SELECT FC.*, F.*, C.*
                      FROM FilmCategories FC
                      JOIN Films F ON FC.FilmId = F.Id
                      JOIN Categories C ON FC.CategoryId = C.Id",
                (filmCategory, film, category) =>
                {
                    filmCategory.Film = film;
                    filmCategory.Category = category;
                    return filmCategory;
                },
                splitOn: "Id,Id"
            );

            return result.AsList();
        }
    }
}
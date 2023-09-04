
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace C__Movies_App_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    public static User user = new User();
    public static Genre genre = new Genre();

    [HttpPost("addFavorite")]
    public async Task<ActionResult<List<Favorite>>> AddFavorite(FavoriteRequest request)
    {
        using var DbContext = new DataContext();
        user = DbContext.Users.FirstOrDefault(user => user.Email == request.Email);
        var exited = DbContext.Favorites.FirstOrDefault(f => f.Url == request.Url && f.User.Email == request.Email);
        if (user != null && exited == null)
        {
            var favorite = new Favorite
            {
                User = user,
                MovieId = request.MovieId,
                Title = request.Title,
                Url = request.Url,
                Rate = request.Rate,
                Year = request.Year,
                Genres = new List<Genre>()
            };
            foreach (var genreData in request.GenreRequest)
            {
                genre = new Genre { Name = genreData.Name, Id = genreData.Id, Favorite = favorite };
                favorite.Genres.Add(genre);
            }
            DbContext.Add(favorite);
            DbContext.SaveChanges();
            List<FavoriteRequest> favoriteList = GetFavorites(request.Email);
            return Ok(favoriteList);
        }
        return NotFound("Couldn't add favorite movie");


    }



    private static void AddFavorite(Favorite favorite)
    {
        using var DbContext = new DataContext();
        DbContext.Favorites.Add(new Favorite()
        {
            MovieId = favorite.MovieId,
            Title = favorite.Title,
            Url = favorite.Url,
            Rate = favorite.Rate,
            Year = favorite.Year,
        });
        DbContext.SaveChanges();
    }

    [HttpPost("removeFavorite")]
    public async Task<ActionResult<string>> removeFavorites(FavoriteRequest favorite)
    {
        using var DbContext = new DataContext();
        var exitedFavorite = DbContext.Favorites.FirstOrDefault(f => f.User.Email == favorite.Email && f.Url == favorite.Url);
        if (exitedFavorite != null)
        {
            DbContext.Favorites.Remove(exitedFavorite);
            DbContext.SaveChanges();
            return Ok("Favorite movie removed");
        }
        var errorResponse = new { ErrorMessage = "Couldn't remove favorite movie" };
        return NotFound(errorResponse);
    }

    private static List<FavoriteRequest> GetFavorites(string email)
    {
        using var DbContext = new DataContext();
        var favorite = DbContext.Favorites.Where(favorite => favorite.User.Email == email)
        .Include(f => f.Genres)
        .Select(f => new FavoriteRequest
        {
            MovieId = f.MovieId,
            Url = f.Url,
            Title = f.Title,
            Rate = f.Rate,
            Year = f.Year,
            GenreRequest = f.Genres.Select(g => new GenreRequest
            {
                Id = g.Id,
                Name = g.Name
            })
      .ToList()
        }).ToList();
        return favorite;
    }
}
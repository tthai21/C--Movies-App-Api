
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace C__Movies_App_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    public static User user = new User();
    public static Favorite favorite = new Favorite();
    [HttpPost("addFavorite")]
    public async Task<ActionResult<List<Favorite>>> Favorite(FavoriteDto request)
    {
        using var DbContext = new DataContext();
        favorite.Email = request.Email;
        favorite.MovieLink = request.MovieLink;
        user = DbContext.Users.FirstOrDefault(user => user.Email == request.Email);
        favorite.UserId = user.UserId;
        var exited = DbContext.Favorites.FirstOrDefault(f => f.MovieLink == favorite.MovieLink && f.Email == favorite.Email);
        if (exited == null)
        {
            AddFavorite(favorite);
            List<Favorite> favoriteList = GetFavorites(request.Email);
            return Ok(favoriteList);
        }
        return NotFound("Couldn't add favorite movie");


    }

    private Task AddFavorite(FavoriteDto favorite)
    {
        throw new NotImplementedException();
    }

    private static void AddFavorite(Favorite favorite)
    {
        using var DbContext = new DataContext();
        DbContext.Favorites.Add(new Favorite()
        {
            MovieLink = favorite.MovieLink,
            Email = favorite.Email,
            UserId = favorite.UserId
        });
        DbContext.SaveChanges();
    }

    [HttpPost("removeFavorite")]
    public async Task<ActionResult<string>> removeFavorites(FavoriteDto favorite)
    {
        using var DbContext = new DataContext();
        var exitedFavorite = DbContext.Favorites.FirstOrDefault(f => f.Email == favorite.Email && f.MovieLink == favorite.MovieLink);
        if (exitedFavorite != null)
        {
            DbContext.Favorites.Remove(exitedFavorite);
            DbContext.SaveChanges();
            return Ok("Favorite movie removed");
        }
        var errorResponse = new { ErrorMessage = "Couldn't remove favorite movie" };
        return NotFound(errorResponse);
    }

    private static List<Favorite> GetFavorites(string Email)
    {
        using var DbContext = new DataContext();
        return DbContext.Favorites.Where(favorite => favorite.Email == Email).ToList();
    }


}
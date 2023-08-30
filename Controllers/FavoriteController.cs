
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

        favorite.Email = request.Email;
        favorite.MovieLink = request.MovieLink;
        using var DbContext = new DataContext();
        var exited = DbContext.Favorites.FirstOrDefault(f => f.MovieLink == favorite.MovieLink && f.Email == favorite.Email);
        if (exited == null)
        {
            AddFavorite(favorite);
            List<Favorite> favoriteList = GetFavorites(request.Email);
            return Ok(favoriteList);
        }
        return BadRequest("Couldn't add favorite movie");


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
            Email = favorite.Email
        });
        DbContext.SaveChanges();
    }

    private static List<Favorite> GetFavorites(string Email)
    {
        using var DbContext = new DataContext();
        return DbContext.Favorites.Where(favorite => favorite.Email == Email).ToList();
    }


}
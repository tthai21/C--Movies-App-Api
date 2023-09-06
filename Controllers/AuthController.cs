using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;


namespace C__Movies_App_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    public static User user = new User();
    // public static Favorite favorite = new Favorite();

    private IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        using var DbContext = new DataContext();
        bool exitedUser = await DbContext.Users.AnyAsync(user => user.Email == request.Email);
        if (exitedUser == true)
        {
            var errorResponse = new { ErrorMessage = "Email already exists" };
            return NotFound(errorResponse);
        }
        var newUser = new User();
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        newUser.Username = request.Username;
        newUser.Email = request.Email;
        newUser.PasswordHash = passwordHash;
        newUser.PasswordSalt = passwordSalt;
        DbContext.Users.Add(newUser);
        await DbContext.SaveChangesAsync();
        return Ok(newUser);
    }


    [HttpPost("login")]

    public async Task<ActionResult<User>> Login(UserLogin request)
    {
        using var dbContext = new DataContext();
        user = dbContext.Users.FirstOrDefault(user => user.Email == request.Email);
        if (user == null)
        {
            var errorResponse = new { ErrorMessage = "Email not found" };
            return NotFound(errorResponse);
        }
        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            var errorResponse = new { ErrorMessage = "Incorrect Password" };
            return NotFound(errorResponse);
        }

        string token = CreateToken(user);
        //    List<FavoriteRequest> favoriteList = GetFavorites(request.Email);


        var respond = new RespondList
        {
            token = token,
            // favoriteList = favoriteList
        };

        return Ok(respond);
    }


    private string CreateToken(User user)
    {

        List<Claim> claims = new List<Claim>()
        {
            new Claim("username", user.Username),
            new Claim("email", user.Email),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var tokenHandler = new JwtSecurityTokenHandler();
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = cred
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private static void InsertUser(User user)
    {
        using var dbContext = new DataContext();
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

    }

}
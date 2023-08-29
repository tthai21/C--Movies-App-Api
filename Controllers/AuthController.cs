using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace C__Movies_App_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    public static User user = new User();

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.Username = request.Username;
        user.Email = request.Email;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return Ok(user);
    }


    [HttpPost("login")]

    public async Task<ActionResult<string>> Login(UserDto request)
    {
        if (user.Email != request.Email)
        {
            return BadRequest("Email not found");
        }
        return Ok("My Token");
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
}
using System.ComponentModel.DataAnnotations;

namespace C__Movies_App_Api
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // public List<Favorite> Favorites { get; set; }
        

    }
}
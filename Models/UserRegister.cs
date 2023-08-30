namespace C__Movies_App_Api
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Gender { get; set; } = null;
        public bool? Terms { get; set; } = null;
    }
}
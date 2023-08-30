using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C__Movies_App_Api
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        public string MovieLink { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // public virtual User User { get; set; }
    }
}
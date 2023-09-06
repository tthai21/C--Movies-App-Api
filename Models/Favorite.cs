using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace C__Movies_App_Api
{
    public class Favorite
    {
        [Key]
        public int FavoriteMovieId { get; set; }
        public int MovieId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public float Rate { get; set; }
        public int Year { get; set; }
        // [ForeignKey("UserId")]
        public User User { get; set; }
        public List<Genre> Genres { get; set; }

    }
}
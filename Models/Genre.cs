using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C__Movies_App_Api
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [ForeignKey("FavoriteMovieId")]
        [Required]
        public Favorite Favorite { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }


    }
}
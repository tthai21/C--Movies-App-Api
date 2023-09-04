using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C__Movies_App_Api
{
    public class GenreRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
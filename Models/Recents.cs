using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C__Movies_App_Api
{
    public class Recent
    {
        [Key]
        public int Id { get; set; }        
        public int UserId { get; set; }
        public string MovieLink { get; set; } = string.Empty;      
    }
}
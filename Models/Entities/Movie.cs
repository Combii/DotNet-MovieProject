using System.ComponentModel.DataAnnotations;

namespace mvcProject.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string MovieName { get; set; }
    }
}
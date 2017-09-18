using System;
using System.ComponentModel.DataAnnotations;

namespace mvcProject.Models.Entities
{
    public class Movie : IComparable<Movie>
    {
        public int Id { get; set; }

        [Required]
        public string MovieName { get; set; }

        public int CompareTo(Movie other)
        {
            if (other != null)
                return string.Compare(MovieName, other.MovieName, StringComparison.Ordinal);
            return 0;
        }
    }
}
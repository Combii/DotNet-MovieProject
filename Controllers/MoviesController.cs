using System.Collections.Generic;
using System.Linq;
using mvcProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace mvcProject.Controllers
{
    public class MoviesController : Controller
    {
        
        MoviesDbContext db = new MoviesDbContext();

        
        public IActionResult Index()
        {
            IEnumerable<Movie> movies = db.Movies.ToList();
            
            return View(movies);
        }   
        
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            Movie movie = getMovie(id);
            
            if (movie != null)
            return View(movie);

            return NotFound();
        }

        private Movie getMovie(int id)
        {
            foreach (var movie in db.Movies.ToList())
            {
                if (movie.Id.Equals(id))
                {
                    return movie;
                }
            }
            return null;
        }
    }
}
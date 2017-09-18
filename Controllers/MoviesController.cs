using System.Collections.Generic;
using System.Linq;
using mvcProject.Models;
using mvcProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace mvcProject.Controllers
{
    public class MoviesController : Controller
    {
        
        MoviesDbContext db = new MoviesDbContext();

        
        public IActionResult Index()
        {
            var movies = db.Movies.ToList();
            
            movies.Sort();
            
            return View(movies);
        }   
        
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid) return View();
            
            db.Movies.Add(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Movie movie = GetMovie(id);
            
            if (movie != null)
            return View(movie);

            return NotFound();
        }

        private Movie GetMovie(int id)
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
using System;
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
        
        [HttpPost]
        public IActionResult Index(string movieName)
        {
            var movies = db.Movies.ToList();
            
            var newFilteredList = new List<Movie>();
            
            foreach (var movie in movies)
            {
                if (movie.MovieName.StartsWith(movieName))
                {
                    newFilteredList.Add(movie);
                    Console.Write(newFilteredList.Count + "\n");
                }
            }

            newFilteredList.Sort();
            
            return View(newFilteredList);
        }

        public IActionResult MovieList()
        {       
            return View();
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

            return RedirectToAction("Index");
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
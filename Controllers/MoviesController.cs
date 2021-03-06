﻿using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(movieName)) return RedirectToAction("Index");

            var movies = db.Movies.ToList();

            var newFilteredList = new List<Movie>();

            foreach (var movie in movies)
            {
                if (movie.MovieName.StartsWith(UppcaseFirstLetter(movieName)))
                {
                    newFilteredList.Add(movie);
                }
            }
            newFilteredList.Sort();
            return View(newFilteredList);
        }

        public IActionResult MovieList()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var movie = GetMovie(id);

            if (movie != null)
                return View(movie);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid) return View();
            movie.MovieName = UppcaseFirstLetter(movie.MovieName);

            db.Movies.Add(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            db.Movies.Remove(GetMovie(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }        
        
        public IActionResult Edit(int movieId)
        {
            return View("MovieEdit", GetMovie(movieId));
        }
        
        public IActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                db.Movies.Add(movie);
            }
            else
            {
                var customerInDb = db.Movies.Single(c => c.Id == movie.Id);
                customerInDb.MovieName = movie.MovieName;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Movies");
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
        
        private string UppcaseFirstLetter(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
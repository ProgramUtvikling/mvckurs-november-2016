using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Utils;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    public class MovieController : Controller
    {
        public ViewResult Index()
        {
			var db = new ImdbDAL.ImdbContext();
			var movies = db.Movies.ToList();
			ViewData.Model = movies;
			return View();
        }

        public ViewResult Details(string id)
        {
			var db = new ImdbDAL.ImdbContext();
			var movie = db.Movies.Find(id);
			ViewData.Model = movie;
			return View();
		}
		public ViewResult Genres()
        {
			var db = new ImdbDAL.ImdbContext();
			var genres = db.Genres.ToList();
			ViewData.Model = genres;
			return View();
		}

		[Route("[Controller]/Genre/{genrename}")]
        public ViewResult MoviesByGenre(string genrename)
        {
			var db = new ImdbDAL.ImdbContext();
			var movies = from movie in db.Movies
						 where movie.Genre.Name == genrename
						 select movie;

			ViewData.Model = movies.WithTitle(genrename);
			return View("Index");
		}
	}
}

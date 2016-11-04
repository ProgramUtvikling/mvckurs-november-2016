using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Utils;
using ImdbDAL;
using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
	//[Authorize]
	public class MovieController : Controller
	{
		private readonly ImdbContext _db;

		public MovieController(ImdbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			var movies = await _db.Movies.ToListAsync();

			ViewData.Model = movies.WithTitle(null);
			return View();
		}

		public async Task<IActionResult> Details(string id)
		{
			var movie = await _db.Movies.FindAsync(id);

			if (movie == null)
			{
				return NotFound();
			}

			ViewData.Model = movie;
			return View();
		}
		public async Task<ViewResult> Genres()
		{
			var genres = await _db.Genres.ToListAsync();
			ViewData.Model = genres;
			return View();
		}

		[Route("[Controller]/Genre/{genrename}")]
		public async Task<IActionResult> MoviesByGenre(string genrename)
		{
			var movies = await (from movie in _db.Movies
								where movie.Genre.Name == genrename
								select movie).ToListAsync();

			ViewData.Model = movies.WithTitle(genrename);
			return View("Index");
		}
	}
}

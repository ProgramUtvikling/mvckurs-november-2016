using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using System.Xml.Linq;
using System.Data.Entity;
using System.Net;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
	public class ImdbApiController : Controller
	{
		private readonly ImdbContext _db;

		public ImdbApiController(ImdbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Movies(string fmt = "xml")
		{
			switch (fmt.ToLower())
			{
				case "xml": return await MoviesAsXml();
				case "json": return await MoviesAsJson();
				default:
					return StatusCode((int)HttpStatusCode.BadRequest);
			}
		}

		public async Task<IActionResult> MoviesAsXml()
		{
			var movies = await _db.Movies.ToListAsync();

			var doc = new XElement("movies",
				from movie in movies
				select new XElement("movie",
					new XAttribute("title", movie.Title),
					new XAttribute("id", movie.MovieId)
					)
				);

			return Content(doc.ToString(), "application/xml");
		}

		public async Task<IActionResult> MoviesAsJson()
		{
			var movies = await _db.Movies.ToListAsync();

			var obj = from movie in movies
					  select new { movie.Title, movie.MovieId };

			return Json(obj);
		}

		[Route("Movie/Details/{id}.xml")]
		public async Task<IActionResult> MovieDetails(string id)
		{
			var movie = await _db.Movies.FindAsync(id);
			if (id == null)
			{
				return NotFound();
			}

			var doc = new XElement("movie",
				new XAttribute("id", movie.MovieId),
				new XAttribute("title", movie.Title),
				new XAttribute("origTitle", movie.OriginalTitle),
				new XAttribute("genre", movie.Genre.Name),
				new XAttribute("prodYear", movie.ProductionYear),
				from p in movie.Actors select new XElement("actor", p.Name),
				from p in movie.Directors select new XElement("director", p.Name),
				from p in movie.Producers select new XElement("producer", p.Name),
				new XCData(movie.Description)
				);

			return Content(doc.ToString(), "application/xml");
		}
	}
}

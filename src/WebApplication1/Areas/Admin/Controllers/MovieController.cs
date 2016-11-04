using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Areas.Admin.Controllers
{
	[Area("Admin"), Authorize]
	public class MovieController : Controller
	{
		private readonly ImdbContext _db;
		public MovieController(ImdbContext db)
		{
			_db = db;
		}

		// GET: /<controller>/
		public async Task<IActionResult> Index()
		{
			ViewData.Model = await _db.Movies.Select(m => new MovieIndexModel
			{
				Id = m.MovieId,
				Title = m.Title,
				RunningLength = m.RunningLength
			}).ToListAsync();
			return View();
		}

		public async Task<IActionResult> Create()
		{
			ViewBag.Genres = new SelectList(await _db.Genres.ToListAsync(), "GenreId", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(MovieCreateModel model)
		{
			if (ModelState.IsValid)
			{
				var movie = new Movie
				{
					MovieId = model.Id,
					Title = model.Title,
					OriginalTitle = model.OriginalTitle,
					Description = model.Description,
					ProductionYear = model.ProductionYear,
					RunningLength = model.RunningLengthHours * 60 + model.RunningLengthMinutes,
					GenreId = model.GenreId
				};
				_db.Movies.Add(movie);
				await _db.SaveChangesAsync();

				return RedirectToAction("Index");
			}

			return await Create();
		}

		public static ValidationResult CheckId(string id, ValidationContext ctx)
		{
			var db = new ImdbContext("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Imdb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
			if (db.Movies.Any(m => m.MovieId == id))
			{
				return new ValidationResult("Filmen er allerede registrert");
			}
			return ValidationResult.Success;
		}
		public async Task<IActionResult> Delete(string id)
		{
			var movie = await _db.Movies.FindAsync(id);
			if (movie == null)
			{
				return NotFound();
			}

			ViewData.Model = new MovieDeleteModel
			{
				Id = movie.MovieId,
				Title = movie.Title,
				OriginalTitle = movie.OriginalTitle,
				ProductionYear = movie.ProductionYear
			};
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public async Task<IActionResult> PerformDelete(string id)
		{
			var movie = await _db.Movies.FindAsync(id);
			if (movie != null)
			{
				_db.Movies.Remove(movie);
				await _db.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}


	}
}

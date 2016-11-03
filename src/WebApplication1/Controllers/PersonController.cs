using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using WebApplication1.Utils;
using System.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
		private readonly ImdbContext _db;

		public PersonController(ImdbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Actors()
        {
			var persons = await (from person in _db.Persons
						  where person.ActedMovies.Any()
						  select person).ToListAsync();

			ViewData.Model = new EnumerableWithTitle<Person>(persons, "Skuespillere");
			return View("Index");
        }
        public async Task<IActionResult> Producers()
        {
			var persons = await _db.Persons.Where(person => person.ProducedMovies.Any()).ToListAsync();

			ViewData.Model = new EnumerableWithTitle<Person>(persons, "produsenter");
			return View("Index");
		}
		public async Task<IActionResult> Directors()
        {
			var persons = await (from person in _db.Persons
						  where person.DirectedMovies.Any()
						  select person).ToListAsync();

			ViewData.Model = new EnumerableWithTitle<Person>(persons, "Regisører");
			return View("Index");
		}

		[Route("[Controller]/{id:int}")]
        public async Task<IActionResult>  Details(int id)
        {
			var person = await _db.Persons.FindAsync(id);

			ViewData.Model = person;
			return View();
		}
	}
}

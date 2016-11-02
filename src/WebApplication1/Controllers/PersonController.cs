using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImdbDAL;
using WebApplication1.Utils;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
        public ViewResult Actors()
        {
			var db = new ImdbDAL.ImdbContext();
			var persons = from person in db.Persons
						  where person.ActedMovies.Any()
						  select person;

			ViewData.Model = new EnumerableWithTitle<Person>(persons, "Skuespillere");
			return View("Index");
        }
        public ViewResult Producers()
        {
			var db = new ImdbDAL.ImdbContext();
			var persons = db.Persons.Where(person => person.ProducedMovies.Any());

			ViewData.Model = new EnumerableWithTitle<Person>(persons, "produsenter");
			return View("Index");
		}
		public ViewResult Directors()
        {
			var db = new ImdbDAL.ImdbContext();
			var persons = from person in db.Persons
						  where person.DirectedMovies.Any()
						  select person;

			ViewData.Model = new EnumerableWithTitle<Person>(persons, "Regisører");
			return View("Index");
		}

		[Route("[Controller]/{id:int}")]
        public ViewResult Details(int id)
        {
			var db = new ImdbDAL.ImdbContext();
			var person = db.Persons.Find(id);

			ViewData.Model = person;
			return View();

		}
	}
}

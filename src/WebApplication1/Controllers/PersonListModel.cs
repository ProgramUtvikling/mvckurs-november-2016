using ImdbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
	public class PersonListModel
	{
		public IEnumerable<Person> Persons { get; set; }
		public string Title { get; set; }
	}
}

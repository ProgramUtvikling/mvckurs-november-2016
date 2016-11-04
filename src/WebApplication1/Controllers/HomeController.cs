using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Security.Application;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		public ViewResult Index()
		{
			return View();
		}

		public IActionResult Demo()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Demo(DemoModel model)
		{
			model.Article = Sanitizer.GetSafeHtmlFragment(model.Article);


			ViewData.Model = model;

			return View("DemoResultView");
		}

	}

	public class DemoModel
	{
		public string Headline { get; set; }
		public string Article { get; set; }
	}
}

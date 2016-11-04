using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
	[Authorize]
	public class AccountController : Controller
    {
		[AllowAnonymous]
		public IActionResult Logon()
		{
			// vise frem påloggingssiden
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logon(LogonModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (model.Username == "arjan" && model.Password == "pass")
				{
					var claims = new[] { new Claim(ClaimTypes.Name, model.Username) };

					var identity = new ClaimsIdentity(claims, "Basic");

					var principal = new ClaimsPrincipal(identity);

					await HttpContext.Authentication.SignInAsync("MinAuthMellomvare", principal,
						new AuthenticationProperties { IsPersistent = model.RememberMe });

					if (string.IsNullOrWhiteSpace(returnUrl))
					{
						return RedirectToAction("Index", "Home");
					}
					return Redirect(returnUrl);

				}
			}
			return View();
		}

		public async Task<IActionResult> SignOut()
		{
			await HttpContext.Authentication.SignOutAsync("MinAuthMellomvare");
			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public IActionResult LogonStatus()
		{
			ViewData.Model = User;
			return View();
		}
	}
}

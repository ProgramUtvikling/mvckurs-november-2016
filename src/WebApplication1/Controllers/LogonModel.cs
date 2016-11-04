using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class LogonModel
    {
		[Required]
		[StringLength(20, MinimumLength = 3)]
		[Display(Name="Brukernavn")]
		public string Username { get; set; }

		[Required]
		[Display(Name = "Passord")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Husk meg på denne maskinen!")]
		public bool RememberMe { get; set; }

	}
}

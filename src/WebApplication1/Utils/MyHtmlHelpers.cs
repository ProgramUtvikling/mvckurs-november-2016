using ImdbDAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public static class MyHtmlHelpers
    {
		public static string PrettyJoin(this IHtmlHelper htmlHelper, IEnumerable<Person> persons)
		{
			string res = "";

			int c = 0;
			foreach (var person in persons.Reverse())
			{
				switch (c++)
				{
					case 0:
						res = person.Name;
						break;
					case 1:
						res = person.Name + " og " + res;
						break;
					default:
						res = person.Name + ", " + res;
						break;
				}
			}


			return res;
		}
    }
}

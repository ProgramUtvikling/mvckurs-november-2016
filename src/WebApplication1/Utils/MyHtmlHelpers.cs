using ImdbDAL;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public static class MyHtmlHelpers
    {
		public static IHtmlContent PrettyJoin(this IHtmlHelper html, IEnumerable<Person> persons)
		{
			Func<Person, IHtmlContent> linkify = person => html.ActionLink(person.Name, "Details", "Person", new { id = person.PersonId }, null);

			var writer = new StringWriter();
			var enc = HtmlEncoder.Default;

			int personsCount = persons.Count();
			int counter = 0;
			foreach (var person in persons)
			{
				counter++;
				if(counter == personsCount)
				{
					writer.Write(" og ");
				}
				else if(counter > 1)
				{
					writer.Write(", ");
				}

				linkify(person).WriteTo(writer, enc);
			}


			return new HtmlString(writer.ToString());
		}
    }
}

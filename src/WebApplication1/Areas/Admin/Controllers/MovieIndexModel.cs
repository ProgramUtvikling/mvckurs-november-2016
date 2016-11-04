using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.Controllers
{
	public class MovieIndexModel
	{
		public string Id { get; internal set; }
		public string Title { get; internal set; }

		[UIHint("Duration")]
		public int RunningLength { get; set; }
	}
}
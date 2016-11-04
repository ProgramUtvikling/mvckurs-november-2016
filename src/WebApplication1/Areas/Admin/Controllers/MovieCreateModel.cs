using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.Controllers
{
	public class MovieCreateModel
	{
		[Display(Name = "EAN kode")]
		[Required]
		[MaxLength(30)]
		[CustomValidation(typeof(MovieController), "CheckId")]
		public string Id { get; set; }

		[Display(Name = "Tittel")]
		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Display(Name = "Original tittel")]
		[MaxLength(100)]
		public string OriginalTitle { get; set; }

		public string Description { get; set; }

		[MaxLength(4)]
		public string ProductionYear { get; set; }

		[Required]
		[Range(0, int.MaxValue / 60 -1)]
		public int RunningLengthHours { get; set; }
		[Required]
		[Range(0, 59)]
		public int RunningLengthMinutes { get; set; }

		[Required]
		public int GenreId { get; set; }
	}
}
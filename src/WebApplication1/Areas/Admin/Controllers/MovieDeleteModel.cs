namespace WebApplication1.Areas.Admin.Controllers
{
	public class MovieDeleteModel
	{
		public string Id { get; internal set; }
		public string Title { get; internal set; }
		public string OriginalTitle { get; set; }
		public string ProductionYear { get; set; }
	}
}
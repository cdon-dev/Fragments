using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Sample.Features.Baseline
{
	[Route("baseline")]
	public class BaselineController : Controller
	{
		[HttpGet("baseline.css.html")]
		[ResponseCache(Duration = 60 * 60)]
		public IActionResult BaselineCss() => PartialView("baseline.css");
	}
}

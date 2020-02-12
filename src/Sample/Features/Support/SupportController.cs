using Microsoft.AspNetCore.Mvc;

namespace Sample.Features.Support
{
	[Route("fragments")]
	public class SupportController : Controller
	{
		[HttpGet("Support.html")]
		[ResponseCache(Duration = 60 * 10)]
		public IActionResult Support() => PartialView("Support");

		[HttpGet("Support.js.html")]
		[ResponseCache(Duration = 60 * 60)]
		public IActionResult SupportJs() => PartialView("Support.js");

		[HttpGet("Support.css.html")]
		[ResponseCache(Duration = 60 * 60)]
		public IActionResult SupportCss() => PartialView("Support.css");
	}
}

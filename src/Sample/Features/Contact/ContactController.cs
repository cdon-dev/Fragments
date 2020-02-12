using Microsoft.AspNetCore.Mvc;

namespace Sample.Features.Contact
{
	[Route("fragments")]
	public class BaselineController : Controller
	{
		[HttpGet("Contact.html")]
		[ResponseCache(Duration = 60 * 10)]
		public IActionResult Contact() => PartialView("Contact");

		[HttpGet("Contact.js.html")]
		[ResponseCache(Duration = 60 * 60)]
		public IActionResult ContactJs() => PartialView("Contact.js");

		[HttpGet("Contact.css.html")]
		[ResponseCache(Duration = 60 * 60)]
		public IActionResult ContactCss() => PartialView("Contact.css");
	}
}

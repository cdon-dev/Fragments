using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Sample.Features.Contact
{
	[Route("fragments")]
	public class BaselineController : Controller
	{
		[HttpGet("Contact.html")]
		[ResponseCache(Duration = 60 * 10)]
		public async Task<IActionResult> Contact() => PartialView("Contact");

		[HttpGet("Contact.js.html")]
		[ResponseCache(Duration = 60 * 60)]
		public async Task<IActionResult> ContactJs() => PartialView("Contact.js");

		[HttpGet("Contact.css.html")]
		[ResponseCache(Duration = 60 * 60)]
		public async Task<IActionResult> ContactCss() => PartialView("Contact.css");
	}
}

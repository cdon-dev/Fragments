using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Features.Support
{
	[Route("fragments")]
	public class SupportController : Controller
	{
		[HttpGet("Support.html")]
		[ResponseCache(Duration = 60 * 10)]
		public async Task<IActionResult> Support() => PartialView("Support");

		[HttpGet("Support.js.html")]
		[ResponseCache(Duration = 60 * 60)]
		public async Task<IActionResult> SupportJs() => PartialView("Support.js");

		[HttpGet("Support.css.html")]
		[ResponseCache(Duration = 60 * 60)]
		public async Task<IActionResult> SupportCss() => PartialView("Support.css");
	}
}

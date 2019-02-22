using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Features.Demo
{
	[Route("fragments")]
	public class DemoController : Controller
	{
		[HttpGet("demo.html")]
		[ResponseCache(Duration = 60 * 10)]
		public async Task<IActionResult> Demo() => PartialView("Demo");

		[HttpGet("demo.js.html")]
		[ResponseCache(Duration = 60 * 60)]
		public async Task<IActionResult> DemoJs() => PartialView("Demo.js");

		[HttpGet("demo.css.html")]
		[ResponseCache(Duration = 60 * 60)]
		public async Task<IActionResult> DemoCss() => PartialView("Demo.css");
	}
}

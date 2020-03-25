using Fragments;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Features.Support
{
    [Route("fragments")]
    public class SupportController : Controller
    {
        [HttpOptions("Support")]
        [Fragment]
        public IActionResult Support() => NoContent();

        [HttpGet("Support.html")]
        [Fragment]
        [ResponseCache(Duration = 60 * 10)]
        public IActionResult SupportHtml() => PartialView("Support");

        [HttpGet("Support.js.html")]
        [ResponseCache(Duration = 60 * 60)]
        public IActionResult SupportJs() => PartialView("Support.js");

        [HttpGet("Support.css.html")]
        [ResponseCache(Duration = 60 * 60)]
        public IActionResult SupportCss() => PartialView("Support.css");
    }
}

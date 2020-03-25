using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

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

    public class FragmentAttribute : ActionFilterAttribute
    {
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var name = (string)context.RouteData.Values["Controller"];
            var f = context.HttpContext.RequestServices.GetService(typeof(FragmentModel[])) as FragmentModel[];
            var r = f.Where(x => x.Id == name.ToLower()).FirstOrDefault();
            context.HttpContext.Response.Headers.Add("x-fragment-html", r.Html);
            context.HttpContext.Response.Headers.Add("x-fragment-css", r.Css);
            context.HttpContext.Response.Headers.Add("x-fragment-js", r.Js);
            return base.OnResultExecutionAsync(context, next);
        }
    }
}

using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Fragments
{
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

using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fragments.Areas.Fragments
{

    public class FragmentsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> IndexAsync([FromServices] IHttpClientFactory f, [FromServices] FragmentModel[] models)
         => PartialView("~/Areas/Fragments/Index.cshtml", await f.CreateClient("fragments").ToFragmentResouceModels(models));

        [HttpGet("frame")]
        public IActionResult Frame([FromQuery] string name, [FromServices] FragmentModel[] models)
          => PartialView("~/Areas/Fragments/Frame.cshtml", models
              .Select(x => new FrameModel
              {
                  Css = x.Css,
                  Js = x.Js,
                  Html = x.Html,
                  Name = x.FragmentGroupName
              })
              .First(x => x.Name == name));
    }
}
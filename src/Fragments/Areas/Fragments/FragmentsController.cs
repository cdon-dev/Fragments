using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
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
        [ValidateModelState]
        public IActionResult Frame([FromQuery] FrameModel model)
          => PartialView("~/Areas/Fragments/Frame.cshtml", model);
    }
}
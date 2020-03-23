using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net.Http;

namespace Fragments.Areas.Fragments
{
    
    public class FragmentsController : Controller
    {

        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> IndexAsync(
            [FromServices] IActionDescriptorCollectionProvider actionDescriptorsProvider,
            [FromServices] IHttpClientFactory clientFactory)
         => PartialView("~/Areas/Fragments/Index.cshtml", await actionDescriptorsProvider
             .ToFragments(clientFactory.CreateClient("fragments").GetSize()));

        [HttpGet("frame")]
        [ValidateModelState]
        public IActionResult Frame([FromQuery] FrameModel model)
          => PartialView("~/Areas/Fragments/Frame.cshtml", model);
    }
}
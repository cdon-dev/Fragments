using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net.Http;

namespace Fragments.Areas.Fragments
{
    
    public class FragmentsController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorsProvider;

        public FragmentsController(IActionDescriptorCollectionProvider actionDescriptorsProvider)
        {
            _actionDescriptorsProvider = actionDescriptorsProvider;
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
         => PartialView("~/Areas/Fragments/Index.cshtml", await _actionDescriptorsProvider
             .ToFragments(new HttpClient { BaseAddress = new System.Uri($"{Request.Scheme}://{Request.Host.Value}") }.GetSize()));

        [HttpGet("frame")]
        [ValidateModelState]
        public IActionResult Frame([FromQuery] FrameModel model)
          => PartialView("~/Areas/Fragments/Frame.cshtml", model);
    }
}
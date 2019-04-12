using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
        public IActionResult Index()
        {
            return PartialView("~/Areas/Fragments/Index.cshtml", _actionDescriptorsProvider.ToFragments());
        }

        [HttpGet("frame")]
        [ValidateModelState]
        public IActionResult Frame([FromQuery] FrameModel model)
        {
            return PartialView("~/Areas/Fragments/Frame.cshtml", model);
        }
    }
}
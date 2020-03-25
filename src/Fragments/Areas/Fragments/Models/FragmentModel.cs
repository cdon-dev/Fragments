using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fragments.Areas.Fragments.Models
{
    public class FragmentResourceModel
    {
        private FragmentResourceModel(FragmentModel model) 
        {
            Html = model.Html;
            Js = model.Js;
            Css = model.Css;
            FragmentGroupName = model.FragmentGroupName;
        }

        public static async Task<FragmentResourceModel> Create(Func<Uri, Task<(long?, string)>> sizeProvider, FragmentModel fm)
        {
            var f = Extensions.GetFragmentResource(sizeProvider);
            return new FragmentResourceModel(fm)
            {
                CssResource = await f(fm.Css, "css.html"),
                JsResource = await f(fm.Js, "js.html"),
                HtmlResource = await f(fm.Html, ".html")
            };
        }
      
        public FragmentResource HtmlResource { get; private set; }
        public FragmentResource CssResource { get; private set; }
        public FragmentResource JsResource { get; private set; }
        public string Html { get; }
        public string Js { get; }
        public string Css { get; }
        public string FragmentGroupName { get; }

        public string Id => FragmentGroupName.Replace("/", string.Empty).ToLowerInvariant();

        public class FragmentResource
        {
            public bool Missing { get; set; } = true;
            public long? Size { get; set; }
            public string Resource { get; set; }
            public string Cache { get; set; }
            public bool FollowsConvetion { get; set; }

        }
    }

    public class FragmentModel
    {
        private const string HtmlType = "html";
        private const string JsType = "js";
        private const string CssType = "css";

        public FragmentModel(string fragmentGroupName, IEnumerable<string> templates)
        {
            FragmentGroupName = fragmentGroupName;
            var fragmentTypes = Extensions.GetFragmentTypes(HtmlType, templates);
            Html = Extensions.GetFragmentType(HtmlType, fragmentTypes);
            Js = Extensions.GetFragmentType(JsType, fragmentTypes);
            Css = Extensions.GetFragmentType(CssType, fragmentTypes);
        }

        public string FragmentGroupName { get; }
        public string Html { get; }
        public string Js { get; }
        public string Css { get; }
        public string Id => FragmentGroupName.Replace("/", string.Empty).ToLowerInvariant();
    }
}
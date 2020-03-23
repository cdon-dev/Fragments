using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fragments.Areas.Fragments.Models
{
    public class FragmentModel
    {
        private const string HtmlType = "html";
        private const string JsType = "js";
        private const string CssType = "css";

        private static readonly Regex FragmentTypeRegex = new Regex(@"/[^.]+\.([^.]+)\.[^.]+$");

        public FragmentModel(string fragmentGroupName, IEnumerable<string> templates)
        {
            FragmentGroupName = fragmentGroupName;
            var fragmentTypes = GetFragmentTypes(templates);
            Html = GetFragmentType(HtmlType, fragmentTypes);
            Js = GetFragmentType(JsType, fragmentTypes);
            Css = GetFragmentType(CssType, fragmentTypes);
        }

        public static async Task<FragmentModel> Create(Func<Uri, Task<(long?, string)>> sizeProvider, string fragmentGroupName, IEnumerable<string> templates)
        {
            var f = GetFragmentResource(sizeProvider);

            var model = new FragmentModel(fragmentGroupName, templates);

            model.CssResource = await f(model.Css, "css.html");
            model.JsResource = await f(model.Js, "js.html");
            model.HtmlResource = await f(model.Html, ".html");

            return model;
        }

        public string FragmentGroupName { get; }
        public string Html { get; }
        public string Js { get; }
        public string Css { get; }

        public FragmentResource HtmlResource { get; private set; }
        public FragmentResource CssResource { get; private set; }
        public FragmentResource JsResource { get; private set; }

        public string Id => FragmentGroupName.Replace("/", string.Empty).ToLowerInvariant();

        public class FragmentResource
        {
            public bool Missing { get; set; } = true;
            public long? Size { get; set; }
            public string Resource { get; set; }
            public string Cache { get; set; }
            public bool FollowsConvetion { get; set; }

        }

        private static Func<string, string, Task<FragmentResource>> GetFragmentResource(Func<Uri, Task<(long?, string)>> p)
         => async (resource, rule) =>
         {
             if (string.IsNullOrWhiteSpace(resource))
                 return new FragmentResource();

             var r = await p(new Uri(resource, UriKind.Relative));
             return new FragmentResource
             {
                 Missing = string.IsNullOrWhiteSpace(resource),
                 FollowsConvetion = resource.ToLower().EndsWith(rule),
                 Size = r.Item1,
                 Cache = r.Item2,
                 Resource = resource
             };
         };

        private static string GetFragmentType(string type, IEnumerable<(string type, string template)> types)
         => types
            .Where(ft => ft.type == type)
            .Select(ft => ft.template)
            .FirstOrDefault() ?? string.Empty;

        private static List<(string type, string template)> GetFragmentTypes(IEnumerable<string> templates)
         => templates
                .Select(t =>
                {
                    var match = FragmentTypeRegex.Match(t);
                    return !match.Success ? (HtmlType, t) : (match.Groups[1].Value, t);
                })
                .ToList();
    }
}
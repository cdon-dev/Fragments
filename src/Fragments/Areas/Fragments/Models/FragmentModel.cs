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

        public static async Task<FragmentModel> Create(Func<string, Task<long?>> p, string fragmentGroupName, IEnumerable<string> templates)
        {
            var f = new FragmentModel(fragmentGroupName, templates);
            f.CssSize = await p(f.Css);
            return f;
        }

        public string FragmentGroupName { get; }
        public string Html { get; }
        public string Js { get; }
        public string Css { get; }
        public long? CssSize { get; private set; }
        
        public string Id => FragmentGroupName.Replace("/", string.Empty).ToLowerInvariant();

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
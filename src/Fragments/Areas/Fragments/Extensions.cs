using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Fragments.Areas.Fragments
{
    public static class Extensions
    {
        private static readonly Regex FragmentsTypeRegex = new Regex(@"^fragments\/([^.]+)", RegexOptions.IgnoreCase);

        public static IReadOnlyCollection<FragmentModel> ToFragments(
            this IActionDescriptorCollectionProvider actionDescriptorsProvider)
        {
            return actionDescriptorsProvider.ActionDescriptors.Items
                .Select(x => (match: FragmentsTypeRegex.Match(x.AttributeRouteInfo?.Template ?? ""),
                    template: x.AttributeRouteInfo?.Template ?? ""))
                .Where(x => !x.template.EndsWith("frame", System.StringComparison.Ordinal))
                .Where(x => x.match.Success)
                .GroupBy(x => x.match.Groups[1].Value)
                .Select(x => new FragmentModel(x.Key, x.Select(f => f.template)))
                .ToList();
        }
    }
}
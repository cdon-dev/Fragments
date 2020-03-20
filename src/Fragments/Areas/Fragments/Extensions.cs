using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Fragments.Areas.Fragments
{
    public static class Extensions
    {
        private static readonly Regex FragmentsTypeRegex = new Regex(@"^fragments\/([^.]+)", RegexOptions.IgnoreCase);

        public static async Task<IReadOnlyCollection<FragmentModel>> ToFragments(
            this IActionDescriptorCollectionProvider actionDescriptorsProvider,
            Func<string, Task<long?>> p)
         => (await Task.WhenAll(actionDescriptorsProvider.ActionDescriptors.Items
                .Select(x => (match: FragmentsTypeRegex.Match(x.AttributeRouteInfo?.Template ?? ""),
                    template: x.AttributeRouteInfo?.Template ?? ""))
                .Where(x => !x.template.EndsWith("frame", System.StringComparison.Ordinal))
                .Where(x => x.match.Success)
                .GroupBy(x => x.match.Groups[1].Value)
                .Select(x => FragmentModel.Create(p, x.Key, x.Select(f => f.template)))
                ))
            .ToList();

        public static Func<string, Task<long?>> Foo(this HttpClient httpClient)
         => async url =>(await httpClient.GetAsync(url)).Content.Headers.ContentLength;
    }
}
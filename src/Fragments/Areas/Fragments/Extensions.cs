using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static Fragments.Areas.Fragments.Models.FragmentResourceModel;

namespace Fragments.Areas.Fragments
{
    public static class Extensions
    {
        private static readonly Regex FragmentsTypeRegex = new Regex(@"^fragments\/([^.]+)", RegexOptions.IgnoreCase);
        private static readonly Regex FragmentTypeRegex = new Regex(@"/[^.]+\.([^.]+)\.[^.]+$");

        public static IReadOnlyCollection<FragmentModel> ToFragments(
        this IActionDescriptorCollectionProvider actionDescriptorsProvider)
            => actionDescriptorsProvider.ActionDescriptors.Items
            .Where(x => !x.EndpointMetadata.OfType<HttpOptionsAttribute>().Any())
         .Select(x => (match: FragmentsTypeRegex.Match(x.AttributeRouteInfo?.Template ?? ""),
             template: x.AttributeRouteInfo?.Template ?? ""))
         .Where(x => !x.template.EndsWith("frame", System.StringComparison.Ordinal))
         .Where(x => x.match.Success)
         .GroupBy(x => x.match.Groups[1].Value)
         .Select(x => new FragmentModel(x.Key, x.Select(f => f.template)))
         .ToList();

        public static Func<Uri, Task<(long?, string)>> GetSize(this HttpClient httpClient)
         => async url =>
         {
             var r = await httpClient.GetAsync(url);
             var size = r.Content.Headers.ContentLength;
             var cache = string.Join(',', r.Headers.CacheControl);
             return (size, cache);
         };

        public static Func<string, string, Task<FragmentResource>> GetFragmentResource(Func<Uri, Task<(long?, string)>> p)
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

        public static string GetFragmentType(string type, IEnumerable<(string type, string template)> types)
         => types
            .Where(ft => ft.type == type)
            .Select(ft => ft.template)
            .FirstOrDefault() ?? string.Empty;

        public static List<(string type, string template)> GetFragmentTypes(string htmlType, IEnumerable<string> templates)
         => templates
                .Select(t =>
                {
                    var match = FragmentTypeRegex.Match(t);
                    return !match.Success ? (htmlType, t) : (match.Groups[1].Value, t);
                })
                .ToList();

        public static Task<FragmentResourceModel[]> ToFragmentResouceModels(this HttpClient httpClient, params FragmentModel[] models)
         => Task.WhenAll(models.Select(x => Create(httpClient.GetSize(), x)));

    }
}
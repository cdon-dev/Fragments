using Fragments.Areas.Fragments;
using Fragments.Areas.Fragments.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fragments
{
    public static class Extensions
    {
        public static void AddFragments(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient("fragments", (sp, c) =>
            {
                var ctx = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                c.BaseAddress = new Uri($"{ctx.Request.Scheme}://{ctx.Request.Host.Value}");
            });
            services.AddSingleton(sp => sp.GetRequiredService<IActionDescriptorCollectionProvider>().ToFragments().ToArray());
        }
    }
}

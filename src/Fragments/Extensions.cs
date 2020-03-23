using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
                c.BaseAddress = new System.Uri($"{ctx.Request.Scheme}://{ctx.Request.Host.Value}");
            });
        }
    }
}

using Burtson.AspNetCore.Middleware.UrlRewriting;
using Microsoft.AspNetCore.Rewrite;
using System.Net;

namespace Microsoft.AspNetCore.Builder
{
    public static class UrlRewriteExtension
    {
        public static IApplicationBuilder UseSSLRewrite(this IApplicationBuilder app)
        {
            app.UseRewriter(new RewriteOptions()
                    .Add(new RedirectWwwRule())
                    .AddRedirectToHttps()
                    .AddRedirect(@"^section1/(.*)", "new/$1", (int)HttpStatusCode.Redirect)
                    .AddRedirect(@"^section/(\\d+)/(.*)", "new/$1/$2", (int)HttpStatusCode.MovedPermanently)
                    .AddRewrite("^feed$", "/?format=rss", skipRemainingRules: false));

            return app;
        }
    }
}

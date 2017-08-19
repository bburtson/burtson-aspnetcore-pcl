using System;
using System.Net;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace Burtson.AspNetCore.Middleware.UrlRewriting
{
    public class RedirectWwwRule : IRule
    {
        public int StatusCode { get; } = (int)HttpStatusCode.MovedPermanently;

        public bool ExcludeLocalHost { get; set; } = true;

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            var host = request.Host;

            if (host.Host.StartsWith("www", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            if (ExcludeLocalHost && string.Equals(host.Host, "localhost", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            var newPath = request.Scheme + "://www." + host.Value + request.PathBase + request.Path + request.QueryString;

            var response = context.HttpContext.Response;

            response.StatusCode = StatusCode;

            response.Headers[HeaderNames.Location] = newPath;

            context.Result = RuleResult.EndResponse;
        }
    }
}
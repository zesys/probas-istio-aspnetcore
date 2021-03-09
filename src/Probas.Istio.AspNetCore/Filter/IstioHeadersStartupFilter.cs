using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Probas.Istio.AspNetCore.Middleware;
using System;

namespace Probas.Istio.AspNetCore.Filter
{
    public class IstioHeadersStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return (builder) =>
            {
                builder.UseMiddleware<IstioHeadersFetcherMiddleware>();
                next(builder);
            };
        }
    }
}

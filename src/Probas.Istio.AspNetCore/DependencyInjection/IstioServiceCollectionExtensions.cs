using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Probas.Istio.AspNetCore;
using Probas.Istio.AspNetCore.Filter;
using Probas.Istio.AspNetCore.Handler;
using Probas.Istio.AspNetCore.Holder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IstioServiceCollectionExtensions
    {
        public static IServiceCollection AddIstioHeaders(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.TryAddTransient<IstioHeadersDelegatingHandler>();

            services.TryAddScoped<IIstioHeadersHolder, IstioHeadersHolder>();

            services.AddSingleton<IHttpMessageHandlerBuilderFilter, IstioHeadersMessageHandlerBuilderFilter>();
            services.AddSingleton<IStartupFilter, IstioHeadersStartupFilter>();

            services.AddScoped<IstioHttpClient>();
            services.AddHttpClient();

            return services;
        }
    }
}

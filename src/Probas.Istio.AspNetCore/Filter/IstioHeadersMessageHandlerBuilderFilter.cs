using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Probas.Istio.AspNetCore.Handler;
using System;

namespace Probas.Istio.AspNetCore.Filter
{
    public class IstioHeadersMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        private readonly ILogger<IstioHeadersDelegatingHandler> _logger;
        private readonly IHttpContextAccessor _accessor;
        public IstioHeadersMessageHandlerBuilderFilter(ILogger<IstioHeadersDelegatingHandler> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            return (builder) =>
            {
                next(builder);
                builder.AdditionalHandlers.Add(new IstioHeadersDelegatingHandler(_logger, _accessor));
            };
        }
    }
}

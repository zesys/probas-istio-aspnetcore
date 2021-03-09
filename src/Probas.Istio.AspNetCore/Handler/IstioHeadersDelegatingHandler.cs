using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Probas.Istio.AspNetCore.Handler
{
    public class IstioHeadersDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<IstioHeadersDelegatingHandler> _logger;
        private readonly IHttpContextAccessor _accessor;
        public IstioHeadersDelegatingHandler(ILogger<IstioHeadersDelegatingHandler> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var istioHeaders = _accessor.HttpContext?.RequestServices.GetService<IIstioHeadersHolder>();

            if (istioHeaders != null)
            {
                AddHeaderIfNotNull(request, IstioHeadersConstants.REQUEST_ID, istioHeaders.RequestId);
                AddHeaderIfNotNull(request, IstioHeadersConstants.B3_TRACE_ID, istioHeaders.B3TraceId);
                AddHeaderIfNotNull(request, IstioHeadersConstants.B3_SPAN_ID, istioHeaders.B3SpanId);
                AddHeaderIfNotNull(request, IstioHeadersConstants.B3_PARENT_SPAN_ID, istioHeaders.B3ParentSpanId);
                AddHeaderIfNotNull(request, IstioHeadersConstants.B3_SAMPLED, istioHeaders.B3Sampled);
                AddHeaderIfNotNull(request, IstioHeadersConstants.B3_FLAGS, istioHeaders.B3Flags);
                AddHeaderIfNotNull(request, IstioHeadersConstants.OT_SPAN_CONTEXT, istioHeaders.OtSpanContext);
            }

            return base.SendAsync(request, cancellationToken);
        }

        private void AddHeaderIfNotNull(HttpRequestMessage request, string headerName, string headerValue)
        {
            if (!string.IsNullOrWhiteSpace(headerValue))
                request.Headers.TryAddWithoutValidation(headerName, headerValue);
            else
                _logger.LogWarning("Not adding header {headerName} to the client. It is null or empty.", headerName);
        }
    }
}

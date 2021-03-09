using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Probas.Istio.AspNetCore.Middleware
{
    public class IstioHeadersFetcherMiddleware
    {
        private readonly RequestDelegate _next;
        public IstioHeadersFetcherMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task InvokeAsync(HttpContext context, IIstioHeadersHolder istioHeaders)
        {
            istioHeaders.RequestId = context.GetHeader(IstioHeadersConstants.REQUEST_ID);
            istioHeaders.B3TraceId = context.GetHeader(IstioHeadersConstants.B3_TRACE_ID);
            istioHeaders.B3SpanId = context.GetHeader(IstioHeadersConstants.B3_SPAN_ID);
            istioHeaders.B3ParentSpanId = context.GetHeader(IstioHeadersConstants.B3_PARENT_SPAN_ID);
            istioHeaders.B3Sampled = context.GetHeader(IstioHeadersConstants.B3_SAMPLED);
            istioHeaders.B3Flags = context.GetHeader(IstioHeadersConstants.B3_FLAGS);
            istioHeaders.OtSpanContext = context.GetHeader(IstioHeadersConstants.OT_SPAN_CONTEXT);

            return _next(context);
        }
    }
}

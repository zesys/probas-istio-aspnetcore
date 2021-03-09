﻿namespace Probas.Istio.AspNetCore.Holder
{
    public class IstioHeadersHolder : IIstioHeadersHolder
    {
        public string RequestId { get; set; }
        public string B3TraceId { get; set; }
        public string B3SpanId { get; set; }
        public string B3ParentSpanId { get; set; }
        public string B3Sampled { get; set; }
        public string B3Flags { get; set; }
        public string OtSpanContext { get; set; }
    }
}

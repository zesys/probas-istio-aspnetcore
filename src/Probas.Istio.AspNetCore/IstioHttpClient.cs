using Probas.Istio.AspNetCore.Handler;
using System.Net.Http;

namespace Probas.Istio.AspNetCore
{
    public class IstioHttpClient : HttpClient
    {
        public IstioHttpClient(IstioHeadersDelegatingHandler handler) : base(handler)
        {
            // 我们需要将InnerHandler设置为默认的HttpClient。
            handler.InnerHandler = new HttpClientHandler();
        }
    }
}

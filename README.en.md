# Probas.Istio.AspNetCore

#### Description
基于Istio的服务网格链路追踪的监控方案，应用程序携带HTTP表头信息，并将其从传入请求传播到任意传出请求。

#### Software Architecture
Software architecture description

#### Instructions(Usage)

1.  Configure in `Program.cs`
```cs
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIstioHeaders();
        }
```

2.  Usage `IHttpClientFactory` or `IstioHttpClient` 
```cs
    public class SomeClass 
    {
        private readonly ILogger<SomeClass> _logger;
        private readonly IstioHttpClient _client;
        private readonly IHttpClientFactory _factory;

        // IstioHttpClient 与 IHttpClientFactory 已注入可直接使用
        public SomeClass(ILogger<SomeClass> logger, IstioHttpClient client, IHttpClientFactory factory)
        {
            _logger = logger;
            _client = client;
            _factory = factory;
        }

        public void Usage()
        {
            var apiurl_8828 = "http://localhost:8828/everything";
            {
                var result = _client.GetStringAsync(apiurl_8828).Result;
                Console.WriteLine(result);
            }
            {
                var client = _factory.CreateClient("api-8828");
                var result = client.GetStringAsync(apiurl_8828).Result;
                Console.WriteLine(result);
            }
        }
    }

```





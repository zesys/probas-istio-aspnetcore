using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Probas.Istio.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace IstioHeader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IstioHttpClient _client;
        private readonly IHttpClientFactory _factory;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IstioHttpClient client, IHttpClientFactory factory)
        {
            _logger = logger;
            _client = client;
            _factory = factory;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation($"Header {IstioHeadersConstants.REQUEST_ID}:{Request.Headers[IstioHeadersConstants.REQUEST_ID]}");
            _logger.LogInformation($"Header {IstioHeadersConstants.B3_TRACE_ID}:{Request.Headers[IstioHeadersConstants.B3_TRACE_ID]}");
            _logger.LogInformation($"Header {IstioHeadersConstants.B3_SPAN_ID}:{Request.Headers[IstioHeadersConstants.B3_SPAN_ID]}");
            _logger.LogInformation($"Header {IstioHeadersConstants.B3_PARENT_SPAN_ID}:{Request.Headers[IstioHeadersConstants.B3_PARENT_SPAN_ID]}");
            _logger.LogInformation($"Header {IstioHeadersConstants.B3_SAMPLED}:{Request.Headers[IstioHeadersConstants.B3_SAMPLED]}");
            _logger.LogInformation($"Header {IstioHeadersConstants.B3_FLAGS}:{Request.Headers[IstioHeadersConstants.B3_FLAGS]}");
            _logger.LogInformation($"Header {IstioHeadersConstants.OT_SPAN_CONTEXT}:{Request.Headers[IstioHeadersConstants.OT_SPAN_CONTEXT]}");

            var apiurl_8828 = "http://localhost:8828/weatherforecast";
            var forecasts = new List<WeatherForecast>();
            {
                var json = _client.GetStringAsync(apiurl_8828).Result;
                var list = JsonSerializer.Deserialize<List<WeatherForecast>>(json);
                forecasts.AddRange(list);
            }
            {
                var client = _factory.CreateClient("api-8828");
                var json = client.GetStringAsync(apiurl_8828).Result;
                var list = JsonSerializer.Deserialize<List<WeatherForecast>>(json);
                forecasts.AddRange(list);
            }
            var rng = new Random();
            forecasts.AddRange(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }));
            return forecasts.ToArray();
        }
    }
}

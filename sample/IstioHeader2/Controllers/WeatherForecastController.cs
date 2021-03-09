using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Probas.Istio.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstioHeader2.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

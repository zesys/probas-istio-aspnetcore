using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Probas.Istio.AspNetCore
{
    public static class HttpContextExtensions
    {
        public static string GetHeader(this HttpContext context, string headerName)
        {
            if (context.Request.Headers.TryGetValue(headerName, out var values))
            {
                var value = values.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
            }
            return null;
        }
    }
}

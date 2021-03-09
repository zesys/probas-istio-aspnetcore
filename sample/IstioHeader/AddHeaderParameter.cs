using Microsoft.OpenApi.Models;
using Probas.Istio.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace IstioHeader
{
    public class AddHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = IstioHeadersConstants.REQUEST_ID,
                In = ParameterLocation.Header,
                Required = true
            });
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = IstioHeadersConstants.B3_TRACE_ID,
                In = ParameterLocation.Header,
                Required = true
            });
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = IstioHeadersConstants.B3_SPAN_ID,
                In = ParameterLocation.Header,
                Required = true
            });
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = IstioHeadersConstants.B3_PARENT_SPAN_ID,
                In = ParameterLocation.Header,
                Required = true
            });
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = IstioHeadersConstants.B3_SAMPLED,
                In = ParameterLocation.Header,
                Required = true
            });
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = IstioHeadersConstants.B3_FLAGS,
                In = ParameterLocation.Header,
                Required = true
            });
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = IstioHeadersConstants.OT_SPAN_CONTEXT,
                In = ParameterLocation.Header,
                Required = true
            });
        }
    }
}

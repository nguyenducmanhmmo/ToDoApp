using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TodoApp.Api.Filter
{

    public class AddAuthorizationHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Add an authorization header parameter to each operation
            operation.Parameters ??= new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Required = false, // You can set this to true if authentication is required for all endpoints
                Description = "Bearer {your_token_here}",
                Schema = new OpenApiSchema { Type = "String" }
            });
        }
    }
}

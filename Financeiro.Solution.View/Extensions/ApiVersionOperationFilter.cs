using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace Financeiro.Solution.View.Extensions
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var existingParam = operation.Parameters.FirstOrDefault(p => p.Name == "api-version");

            if (existingParam == null)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "api-version",
                    In = ParameterLocation.Query, // Certifica que vai para a query string
                    Required = false, // O parâmetro não é obrigatório
                    Description = "Versão da API",
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    }
                });
            }
        }
    }
}

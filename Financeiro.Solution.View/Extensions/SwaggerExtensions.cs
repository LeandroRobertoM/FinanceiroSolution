using Financeiro.Solution.View.Extensions;
using Microsoft.OpenApi.Models;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "API Controle Financeiro",
                Description = "API Template Financeiro"
            });

            // Registra o filtro de versão da API
            c.OperationFilter<ApiVersionOperationFilter>();
        });
    }

    public static void ConfigSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Controle Financeiro Config");
            c.DefaultModelsExpandDepth(-1);
            c.RoutePrefix = string.Empty; // Garante que a UI do Swagger aparece na raiz
        });
    }
}
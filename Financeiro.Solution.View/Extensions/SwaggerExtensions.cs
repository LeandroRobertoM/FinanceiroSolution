using Microsoft.OpenApi.Models;

namespace Financeiro.Solution.View.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            // Registra o Swagger para documentar a API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Template NDD",
                    Description = "API Template Starship"
                });
            });
        }

        public static void ConfigSwagger(this IApplicationBuilder app)
        {
            // Habilita o Middleware do Swagger.
            app.UseSwagger();


            //Configura o Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "API Controle Financeiro Config");
                c.DefaultModelsExpandDepth(-1);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}

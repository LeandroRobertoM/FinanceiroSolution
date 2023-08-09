using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Migrations.Migrations;
using Financeiro.Solution.Infra.Data.Repositorio;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog;
using Serilog.Events;
using System.Threading.Tasks;
using Financeiro.Solution.View.Extensions;

namespace Financeiro.Solution.View
{
    public class Startup
    {

       //teste view
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {


            services.AddSingleton<DapperContext>();
            services.AddSingleton<Database>();
            services.AddSwagger();

            // para poder Registrar UsuarioSistemaFinanceiroRepository como um serviço
            services.AddScoped<UsuarioSistemaFinanceiroRepository>();
            services.AddScoped<CategoriaRepository>();
            services.AddScoped<DespesaRepository>();
            

            services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSqlServer2012()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("SqlConnection"))
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
            services.AddControllers();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Log.Information("Configuring Swagger...");
        
                app.UseSwagger();
                app.UseSwaggerUI();

            // Configurar o Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            app.UseSerilogRequestLogging(); // Adicionar o middleware do Serilog para registrar as requisições HTTP

         


            // configurando 
          

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigSwagger();
        }
    }
}



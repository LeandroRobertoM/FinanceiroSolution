using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using System;
using System.IO;
using System.Reflection;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Migrations.Migrations;
using Financeiro.Solution.Infra.Data.Migrations.Extensions; // Importe a classe MigrationManager
using Microsoft.Extensions.Hosting;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using Financeiro.Solution.Infra.Data.Repositorio;
using FinanceiroSolution.Domain.Interfaces.ICategoria;

namespace Financeiro.Solution.Infra.Tests
{
   public class StartupTestes
    {
        private readonly IConfiguration _configuration;
        private readonly IHost _host; // Adicione esta linha

        public StartupTestes(IConfiguration configuration, IHost host) // Adicione o parâmetro IHost ao construtor
        {
            _configuration = configuration;
            _host = host; // Atribua o valor do parâmetro _host
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // ... Outros registros de serviços

            // Registro dos serviços de domínio
            services.AddSingleton<ICategoriaServico, CategoriaServico>();



            // ... Outros serviços de Repositorio 

            services.AddSingleton<InterfaceCategoria, CategoriaRepository>();
        }

        // Método para configurar migrações
        public void ConfigureMigrations()
        {
            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddSqlServer2012()
                    .WithGlobalConnectionString(_configuration.GetConnectionString("SqlConnection"))
                    .ScanIn(Assembly.GetExecutingAssembly(), typeof(InitialSeed_20230426_CategoriaTable).Assembly)
                    .For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();

            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            try
            {
                Console.WriteLine("Iniciando as migrações...");
                runner.ListMigrations();
                runner.MigrateUp();
                Console.WriteLine("Migrações concluídas com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante as migrações: {ex}");
                throw;
            }
        }

        // Método para executar as migrações
        public void ExecuteMigrations()
        {
            // Chamar o método MigrateDatabase diretamente
            MigrationManager.MigrateDatabase(_host, _configuration);
        }
    }
}

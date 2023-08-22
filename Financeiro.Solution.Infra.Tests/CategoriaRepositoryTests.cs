using NUnit.Framework;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using System.Threading.Tasks;
using Financeiro.Solution.Infra.Data.Repositorio;
using FinanceiroSolution.Domain.Entidades;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using FluentMigrator.Runner;
using Microsoft.Extensions.Hosting;
using Financeiro.Solution.Infra.Data.Migrations.Extensions;
using System;
using System.Data.SqlClient;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using Assert = Xunit.Assert;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using Microsoft.Extensions.Configuration;
using Financeiro.Solution.Infra.Data.Migrations.Migrations;
using Xunit;
using FluentAssertions.Common;

namespace Financeiro.Solution.Testes
{
    public class CategoriaRepositoryTests
    {
        private InterfaceCategoria _categoriaRepository;
        private IServiceProvider _serviceProvider;

        [SetUp] // Método de configuração a ser executado antes de cada teste
        public void SetUp()
        {
            // Configurar o ambiente de teste
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json")
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<ICategoriaServico, CategoriaServico>();
            services.AddSingleton<InterfaceCategoria, CategoriaRepository>();

            _serviceProvider = services.BuildServiceProvider();
            _categoriaRepository = _serviceProvider.GetRequiredService<InterfaceCategoria>();

            // Obter a string de conexão do arquivo de configuração
            string connectionString = configuration.GetConnectionString("SqlConnection");
            string databaseName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

            // Criar o banco de dados de teste se necessário
            var dapperContext = new DapperContext(configuration);
            var databaseService = new Database(dapperContext);
            databaseService.CreateDatabase(databaseName);

            // Executar as migrações no banco de dados de teste
            var host = new HostBuilder().Build();
            host.MigrateDatabase(configuration);
        }

     /*   [Fact]
        public async Task TestAdicionarCategoria()
        {
            Console.WriteLine("Iniciando o teste TestAdicionarCategoria");

            // Arrange
            var novaCategoria = new Categoria
            {
                Nome = "Categoria Teste1",
                IdSistema = 1
            };

            var categoriaServico = new CategoriaServico(null, null); // Passe os parâmetros necessários
            var resultadoEsperado = true; // Simulando um sucesso

            // Act
            Console.WriteLine("Chamando o método Adicionar");
            var resultado = await categoriaServico.AdicionarCategoria(novaCategoria);

            // Assert
            Console.WriteLine("Verificando o resultado");
            Assert.True(resultado, "A adição da categoria falhou.");

            Console.WriteLine("Teste TestAdicionarCategoria concluído");
        }
     */

    }
}

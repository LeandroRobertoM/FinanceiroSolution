using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio;
using FinanceiroSolution.Domain.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Financeiro.Solution.Infra.Tests
{
    public class CategoriaRepositoryTests
    {
        private CategoriaRepository CreateRepository()
        {
            // Configurar manualmente a configuração simulada
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            configuration["SqlConnection"] = "server=DESKTOP-RT242MK\\SQLEXPRESS; database=SistemaFinanceiro12345; Integrated Security=true;User ID=sa;Password=Core@2023;TrustServerCertificate=True";

            // Criar o mock do DapperContext e configurar o comportamento
            var dbContextMock = new Mock<DapperContext>(configuration);
            dbContextMock.Setup(x => x.CreateConnection()).Returns(new SqlConnection("connectionString"));

            return new CategoriaRepository(dbContextMock.Object);
        }
    }
}

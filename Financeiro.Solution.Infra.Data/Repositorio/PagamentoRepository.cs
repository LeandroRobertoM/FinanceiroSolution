using Dapper;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio.Generics;
using Financeiro.Solution.Infra.Data.Response;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IPagamento;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Repositorio
{
    public class PagamentoRepository : RepositoryGenerics<Pagamento>, InterfacePagamento
    {
        private readonly DapperContext _context;

        public PagamentoRepository(DapperContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IResposta<bool>> AdicionarPagamento(Pagamento pagamento)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var properties = typeof(Categoria).GetProperties().Where(p => p.Name != "mensagem" && p.Name != "Id" && p.Name != "IdCategoria" && p.Name != "IdPagamento" && p.Name != "IdCategoria" && p.Name != "SistemaFinanceiro" && p.Name != "NomePropriedade");

                    var fieldNames = string.Join(", ", properties.Select(p => p.Name));
                    var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
                    var query = $"INSERT INTO {typeof(Pagamento).Name} ({fieldNames}) VALUES ({parameterNames})";
                    var parameters = new DynamicParameters();

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(pagamento);
                        parameters.Add(property.Name, value);
                    }

                    await connection.ExecuteAsync(query, parameters);
                }

                return new Resposta<bool>(true, null);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
                return new Resposta<bool>(false, ex.Message);
            }
        }

        public Task<IList<Despesa>> ListarPagamentosNaoPagasMesesAnterior(string emailUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Despesa>> ListarPagamentosUsuario(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }


}

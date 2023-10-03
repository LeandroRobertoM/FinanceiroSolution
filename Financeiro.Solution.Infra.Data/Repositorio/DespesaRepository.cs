using Dapper;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio.Generics;
using Financeiro.Solution.Infra.Data.Response;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Repositorio
{
    public class DespesaRepository : RepositoryGenerics<Despesa>, InterfaceDespesa
    {
        private readonly DapperContext _context;


        public DespesaRepository(DapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IResposta<bool>> AdicionarDespesa(Despesa despesa)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var properties = typeof(Despesa).GetProperties().Where(p => p.Name != "IdDespesa" && p.Name != "Nome" && p.Name != "IdUser" && p.Name != "mensagem" && p.Name != "Id" && p.Name != "Categoria" && p.Name != "SistemaFinanceiro" && p.Name != "NomePropriedade");
                    var fieldNames = string.Join(", ", properties.Select(p => p.Name));
                    var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
                    var query = $"INSERT INTO {typeof(Despesa).Name} ({fieldNames}) VALUES ({parameterNames})";
                    var parameters = new DynamicParameters();

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(despesa);
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

        public Task<IList<Despesa>> ListarDespesasNaoPagasMesesAnterior(string emailUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Despesa>> ListarDespesasUsuario(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}

using Dapper;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio.Generics;
using Financeiro.Solution.Infra.Data.Response;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
using FinanceiroSolution.Domain.Interfaces.IDespesa.IDespesaRecorrente;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Repositorio
{
    public class DespesaRecorrenciaRepository : RepositoryGenerics<DespesaRecorrencia>, InterfaceDespesaRecorrente
    {
        private readonly DapperContext _context;


        public DespesaRecorrenciaRepository(DapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IResposta<bool>> AdicionarDespesaRecorrente(DespesaRecorrencia despesaRecorrencia)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var properties = typeof(Despesa).GetProperties().Where(p => p.Name != "IdDespesa" && p.Name != "mensagem" && p.Name != "Id" && p.Name != "Categoria" && p.Name != "SistemaFinanceiro" && p.Name != "NomePropriedade" && p.Name != "DataPagamento");

                    var fieldNames = string.Join(", ", properties.Select(p => p.Name));
                    var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
                    var query = $"INSERT INTO {typeof(Despesa).Name} ({fieldNames}) VALUES ({parameterNames})";
                    var parameters = new DynamicParameters();

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(despesaRecorrencia);
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
 

        public Task<IList<DespesaRecorrencia>> ListarDespesasNaoPagasMesesAnterior(string emailUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<DespesaRecorrencia>> ListarDespesasUsuario(string emailUsuario)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"
                        SELECT d.*
                            FROM SistemaFinanceiro s
                            INNER JOIN Categoria c ON s.Id = c.IdSistema
                            INNER JOIN UsuarioSistemaFinanceiro us ON s.Id = us.IdSistema
                            INNER JOIN Despesa d ON c.IdCategoria= d.categoriaId
                            WHERE us.EmailUsuario = @EmailUsuario  AND s.Mes = d.Mes AND s.Ano = d.Ano";

                    var parametros = new { EmailUsuario = emailUsuario };

                    // Log de informações relevantes
                    Console.WriteLine($"Executando a consulta SQL: {query}");
                    Console.WriteLine($"Parâmetros: EmailUsuario = {emailUsuario}");

                    // Executar a consulta
                    var resultado = await connection.QueryAsync<DespesaRecorrencia>(query, parametros);

                    // Log do resultado
                    Console.WriteLine($"Número de categorias encontradas: {resultado}");

                    return resultado.ToList();
                }
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine($"Erro ao listar categorias do usuário: {ex.Message}");
                throw;
            }

        }
    }
    
}

using Dapper;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio.Generics;
using Financeiro.Solution.Infra.Data.Response;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Repositorio
{
    public class SistemaFinanceiroRepository : RepositoryGenerics<SistemaFinanceiro>, InterfaceSistemaFinanceiro
    {
        private readonly DapperContext _context;


        public SistemaFinanceiroRepository(DapperContext context) : base(context)
        {

           
            _context = context;

        }


        /// <summary>
        /// Este método retorna um objeto criado o padrão quando precisa dos dados para inserçã o 
        /// </summary>
        /// <param name="sistemaFinanceiro"></param>
        /// <returns></returns>
        public async Task<IResposta<(bool success, int IdSistemaFinanceiro, SistemaFinanceiro sistemaFianceiroObject)>> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
        {
            int IdSistemaFinanceiro = 0;
            SistemaFinanceiro sistemaFianceiroObject = null;

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var properties = typeof(SistemaFinanceiro).GetProperties().Where(p => p.Name != "mensagem" && p.Name != "Id" && p.Name != "IdCategoria" && p.Name != "IdCategoria" && p.Name != "SistemaFinanceiro" && p.Name != "NomePropriedade");

                    var fieldNames = string.Join(", ", properties.Select(p => p.Name));
                    var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
                    var query = $"INSERT INTO {typeof(SistemaFinanceiro).Name} ({fieldNames}) VALUES ({parameterNames}); SELECT CAST(SCOPE_IDENTITY() AS INT)";
                    var parameters = new DynamicParameters();

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(sistemaFinanceiro);
                        parameters.Add(property.Name, value);
                    }

                    IdSistemaFinanceiro = await connection.ExecuteScalarAsync<int>(query, parameters);
                }

                sistemaFianceiroObject = new SistemaFinanceiro
                {
                    Id = IdSistemaFinanceiro,
                };

                var resposta = new Resposta<(bool success, int IdSistemaFinanceiro, SistemaFinanceiro sistemaFianceiroObject)>(true, "Criado com sucesso!", (true, IdSistemaFinanceiro, sistemaFianceiroObject));
                return resposta;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
                var respostaErro = new Resposta<(bool success, int insertedId, SistemaFinanceiro sistemaFianceiroObject)>(false, ex.Message, (false, 0, null));
                return respostaErro;
            }
        }


        public async Task<IList<SistemaFinanceiro>> ListaSistemasUsuario(string emailUsuario)
        {

            //Ajustar esta Query
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"
                    SELECT c.*
                    FROM SistemaFinanceiro s
                    JOIN Categoria c ON s.Id = c.IdSistema
                    JOIN UsuarioSistemaFinanceiro us ON s.Id = us.IdSistema
                    WHERE us.EmailUsuario = @EmailUsuario AND us.SistemaAtual = 1";

                    var parametros = new { EmailUsuario = emailUsuario };
                    return (await connection.QueryAsync<SistemaFinanceiro>(query, parametros)).ToList();
                }
            }
            catch (Exception ex)
            {
                // Tratar ou relatar a exceção
                Console.WriteLine($"Erro ao listar categorias do usuário: {ex.Message}");
                throw;
            }

        }
 
    }
}

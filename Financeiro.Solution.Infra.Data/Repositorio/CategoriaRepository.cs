 using Dapper;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio.Generics;
using Financeiro.Solution.Infra.Data.Response;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Repositorio
{
    public class CategoriaRepository : RepositoryGenerics<Categoria>, InterfaceCategoria
    {
        private readonly DapperContext _context;

        public CategoriaRepository(DapperContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IResposta<bool>> Adicionar(Categoria categoria)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var properties = typeof(Categoria).GetProperties().Where(p => p.Name != "mensagem" && p.Name != "Id" && p.Name != "IdCategoria" && p.Name != "IdCategoria" && p.Name != "SistemaFinanceiro" && p.Name != "NomePropriedade");

                    var fieldNames = string.Join(", ", properties.Select(p => p.Name));
                    var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
                    var query = $"INSERT INTO {typeof(Categoria).Name} ({fieldNames}) VALUES ({parameterNames})";
                    var parameters = new DynamicParameters();

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(categoria);
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

        public async Task<IList<Categoria>> ListarCategoriasUsuario(string emailUsuario)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"
                        SELECT c.*
                        FROM Categoria c
                        INNER JOIN SistemaFinanceiro s ON c.IdSistema = s.Id
                        INNER JOIN UsuarioSistemaFinanceiro us ON s.Id = us.IdSistema
                        WHERE us.EmailUsuario = @EmailUsuario AND us.SistemaAtual = 1";

                    var parametros = new { EmailUsuario = emailUsuario };

                    // Log de informações relevantes
                    Console.WriteLine($"Executando a consulta SQL: {query}");
                    Console.WriteLine($"Parâmetros: EmailUsuario = {emailUsuario}");

                    // Executar a consulta
                    var resultado = await connection.QueryAsync<Categoria>(query, parametros);

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

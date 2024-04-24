using Dapper;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio.Generics;
using Financeiro.Solution.Infra.Data.Response;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Repositorio
{
    public class UsuarioSistemaFinanceiroRepository : RepositoryGenerics<UsuarioSistemaFinanceiro>, InterfaceUserSistemaFinanceiro
    {
        private readonly DapperContext _context;




        public UsuarioSistemaFinanceiroRepository(DapperContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IResposta<bool>> AdicionarListaSistemaFinanceiro(List<UsuarioSistemaFinanceiro> usuariosSistemas)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var properties = typeof(UsuarioSistemaFinanceiro).GetProperties().Where(p => p.Name != "mensagem" && p.Name != "Id" && p.Name != "IdCategoria" && p.Name != "IdCategoria" && p.Name != "SistemaFinanceiro" && p.Name != "NomePropriedade");

                    var fieldNames = string.Join(", ", properties.Select(p => p.Name));
                    var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
                    var query = $"INSERT INTO {typeof(UsuarioSistemaFinanceiro).Name} ({fieldNames}) VALUES ({parameterNames})";

                    foreach (var usuarioSistema in usuariosSistemas)
                    {
                        var parameters = new DynamicParameters();
                        foreach (var property in properties)
                        {
                            var value = property.GetValue(usuarioSistema);
                            parameters.Add(property.Name, value);
                        }
                        await connection.ExecuteAsync(query, parameters);
                    }
                }

                return new Resposta<bool>(true, null);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
                return new Resposta<bool>(false, ex.Message);
            }
        }

        public async Task<IList<UsuarioSistemaFinanceiro>> ListarUsuariosSistema(int Id)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"SELECT * FROM UsuarioSistemaFinanceiro WHERE Id = @Id";


                    var parametros = new { Id = Id };
                    return (await connection.QueryAsync<UsuarioSistemaFinanceiro>(query, parametros)).ToList();
                }
            }
            catch (Exception ex)
            {
                // Tratar ou relatar a exceção
                Console.WriteLine($"Erro ao listar Usuario Por email do usuário Sistema: {ex.Message}");
                throw;
            }
        }

    

        public async Task<IList<UsuarioSistemaFinanceiro>> ObterUsuarioPorEmail(string emailUsuario)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"SELECT * FROM UsuarioSistemaFinanceiro WHERE EmailUsuario = @EmailUsuario";


                    var parametros = new { EmailUsuario = emailUsuario };
                    return (await connection.QueryAsync<UsuarioSistemaFinanceiro>(query, parametros)).ToList();
                }
            }
            catch (Exception ex)
            {
                // Tratar ou relatar a exceção
                Console.WriteLine($"Erro ao listar Usuario Por email do usuário Sistema: {ex.Message}");
                throw;
            }
        }


        public async Task RemoveUsuarios(List<UsuarioSistemaFinanceiro> usuarios)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"DELETE FROM UsuarioSistemaFinanceiro WHERE EmailUsuario = @EmailUsuario";

                    var parametros = new { EmailUsuario = usuarios };

                    await connection.ExecuteAsync(query, parametros);
                }
            }
            catch (Exception ex)
            {
                // Tratar ou relatar a exceção
                Console.WriteLine($"Erro ao excluir Usuário Por email do usuário Sistema: {ex.Message}");
                throw;
            }
        }
    }
}


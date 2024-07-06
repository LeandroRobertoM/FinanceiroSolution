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
    public class UsuarioCriadorRepository : RepositoryGenerics<UsuarioCreate>, InterfaceUsuarioCreate
    {
        private readonly DapperContext _context;

        public UsuarioCriadorRepository(DapperContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IResposta<bool>> Adicionar(UsuarioCreate usuarioCreateUsuario)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var properties = typeof(UsuarioCreate).GetProperties().Where(p => p.Name != "mensagem" && p.Name != "Id" && p.Name != "IdCategoria" && p.Name != "IdCategoria" && p.Name != "SistemaFinanceiro" && p.Name != "NomePropriedade");

                    var fieldNames = string.Join(", ", properties.Select(p => p.Name));
                    var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
                    var query = $"INSERT INTO {typeof(UsuarioCreate).Name} ({fieldNames}) VALUES ({parameterNames})";
                    var parameters = new DynamicParameters();

                    foreach (var property in properties)
                    {
                        var value = property.GetValue(usuarioCreateUsuario);
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

        public async Task<IList<UsuarioCreate>> ListaSistemasUsuario(string usuarioCriadorId)
        {

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"SELECT * FROM UsuarioCreate WHERE UsuarioCriadorId = @UsuarioCriadorId";


                    var parametros = new { UsuarioCriadorId = usuarioCriadorId };
                    return (await connection.QueryAsync<UsuarioCreate>(query, parametros)).ToList();
                }
            }
            catch (Exception ex)
            {
                // Tratar ou relatar a exceção
                Console.WriteLine($"Erro ao listar Usuario Por email do usuário Sistema: {ex.Message}");
                throw;
            }

        }
    }
}



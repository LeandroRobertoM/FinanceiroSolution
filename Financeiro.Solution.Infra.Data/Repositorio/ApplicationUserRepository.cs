using Financeiro.Solution.Infra.Data.Migrations.Context;
using Dapper;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio.Generics;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.IApplicationUser;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financeiro.Solution.Infra.Data.Response;
using FinanceiroSolution.Domain.Interfaces.IResposta;
using Microsoft.Data.SqlClient;

namespace Financeiro.Solution.Infra.Data.Repositorio
{


    public class ApplicationUserRepository : RepositoryGenerics<ApplicationUser>, InterfaceApplicationUser
    {
        private readonly DapperContext _context;

        public ApplicationUserRepository(DapperContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IList<ApplicationUser>> ListarUsuarioCpf(string CpfUsuario)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"
                        SELECT us.Id,us.UserName,us.Email,us.USR_CPF
                        FROM AspNetUsers us
                        WHERE us.USR_CPF = @USR_CPF";



                    var parametros = new { USR_CPF = CpfUsuario };

                    // Log de informações relevantes
                    Console.WriteLine($"Executando a consulta SQL: {query}");
                    Console.WriteLine($"Parâmetros: USR_CPF = {CpfUsuario}");

   

                    return (await connection.QueryAsync<ApplicationUser>(query, parametros)).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine($"Erro ao listar categorias do usuário: {ex.Message}");
                throw;
            }
        }

        public async Task<IList<ApplicationUser>> ListarUsuarioEmail(string EmailUsuario)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    string query = @"
                        SELECT us.Id,us.UserName,us.Email,us.USR_CPF
                        FROM AspNetUsers us
                        WHERE us.Email = @Email";



                    var parametros = new { Email = EmailUsuario };

                    // Log de informações relevantes
                    Console.WriteLine($"Executando a consulta SQL: {query}");
                    Console.WriteLine($"Parâmetros: USR_CPF = {EmailUsuario}");

                    // Executar a consulta
                    var resultado = await connection.QueryAsync<ApplicationUser>(query, parametros);

                    // Log do resultado
                    Console.WriteLine($"Número de Usuarios encontradas: {resultado}");

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

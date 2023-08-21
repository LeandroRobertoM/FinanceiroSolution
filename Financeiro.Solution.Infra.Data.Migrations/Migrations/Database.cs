using Dapper;
using Financeiro.Solution.Infra.Data.Migrations;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    public class Database
    {
        private readonly DapperContext _context;

        public Database(DapperContext context)
        {
            _context = context;
        }

        public void CreateDatabase(string dbName)
        {
            try
            {
                var query = "SELECT * FROM sys.databases WHERE name = @name";
                var parameters = new DynamicParameters();
                parameters.Add("name", dbName);

                using (var connection = _context.CreateMasterConnection())
                {
                    try
                    {
                        var records = connection.Query(query, parameters);
                        if (!records.Any())
                            connection.Execute($"CREATE DATABASE {dbName}");
                    }
                    catch (SqlException ex)
                    {
                        // Trate exceções específicas do SQL Server aqui
                        Console.WriteLine($"Erro SQL: {ex.Message}");
                        // Ou lance uma exceção personalizada para sinalizar o erro
                        throw new ApplicationException("Erro ao executar consulta ou comando SQL", ex);
                    }
                    catch (Exception ex)
                    {
                        // Trate outras exceções genéricas aqui
                        Console.WriteLine($"Erro: {ex.Message}");
                        // Ou lance uma exceção personalizada para sinalizar o erro
                        throw new ApplicationException("Erro ao executar consulta ou comando SQL", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate exceções que podem ocorrer fora do bloco using
                Console.WriteLine($"Erro externo: {ex.Message}");
                throw new ApplicationException("Erro ao criar o banco de dados", ex);
            }
        }

    }
}

using Dapper;
using Financeiro.Solution.Infra.Data.Context;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using FinanceiroSolution.Domain.Generics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Repositorio.Generics
{
    public class RepositoryGenerics<T> : InterfaceGeneric<T>, IDisposable where T : class
    {

        protected readonly DapperContext _context;

        public RepositoryGenerics(DapperContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Add(T obj)
        {
            using IDbConnection db = _context.CreateConnection();
            var properties = typeof(T).GetProperties().Where(p => p.Name != "NomePropriedade" && p.Name != "mensagem" && p.Name != "Id"&& p.Name!= "SistemaFinanceiro");

            var fieldNames = string.Join(", ", properties.Select(p => p.Name));
            var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));

            var query = $"INSERT INTO {typeof(T).Name} ({fieldNames}) VALUES ({parameterNames})";

            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                parameters.Add(property.Name, value);
            }

            try
            {
                await db.ExecuteAsync(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
                // Ou exiba a mensagem em uma caixa de diálogo, logue o erro, etc.
            }
        }
        public async Task<List<T>> GetAll()
        {
            using IDbConnection db = _context.CreateConnection();
            return (List<T>)await db.QueryAsync<T>($"SELECT * FROM {typeof(T).Name}");
        }

        public async Task<T> GetById(int id)
        {
            using IDbConnection db = _context.CreateConnection();
            return await db.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {typeof(T).Name} WHERE Id = @id", new { id });
        }

        public async Task Delete(T obj)
        {
            using IDbConnection db = _context.CreateConnection();
            await db.ExecuteAsync($"DELETE FROM {typeof(T).Name} WHERE Id = @id", new { obj });
        }

        public async Task Update(T obj)
        {
            using IDbConnection db = _context.CreateConnection();
            await db.ExecuteAsync($"UPDATE {typeof(T).Name} SET property1 = @property1, property2 = @property2 WHERE Id = @id", obj);
        }
    }

}

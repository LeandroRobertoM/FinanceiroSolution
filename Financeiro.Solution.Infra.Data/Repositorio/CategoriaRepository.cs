﻿using Dapper;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Repositorio.Generics;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
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
        public async Task<IList<Categoria>> ListarCategoriasUsuario(string emailUsuario)
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
                    return (await connection.QueryAsync<Categoria>(query, parametros)).ToList();
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

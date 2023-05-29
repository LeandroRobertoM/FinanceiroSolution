
using Financeiro.Solution.Infra.Data.Migrations.Context;
using FinanceiroSolution.Domain.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Configuracao
{
    public class EntityFramework : IdentityDbContext<ApplicationUser>
    {
        public EntityFramework(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SistemaFinanceiro> SistemaFinanceiro { set; get; }
        public DbSet<UsuarioSistemaFinanceiro> UsuarioSistemaFinanceiro { set; get; }
        public DbSet<Categoria> Categoria { set; get; }
        public DbSet<Despesa> Despesa { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            try
            {
                builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

                base.OnModelCreating(builder);
            }
            catch (Exception ex)
            {
                // Faça algo com a exceção, como registrar ou tratar o erro
                Console.WriteLine("Ocorreu um erro durante o método OnModelCreating: " + ex.Message);
                // Ou, se estiver usando o logging do ASP.NET Core:
                // _logger.LogError(ex, "Ocorreu um erro durante o método OnModelCreating");
            }

        }


        public string ObterStringConexao2()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EntityFramework>();
            optionsBuilder.UseSqlServer(); // Substitua "UseSqlServer" pelo provedor de banco de dados que você está utilizando, se for diferente

            var dbContextOptions = optionsBuilder.Options;

            using (var dbContext = new EntityFramework(dbContextOptions))
            {
                var connectionString = dbContext.Database.GetDbConnection().ConnectionString;
                return connectionString;
            }

        }

       public string ObterStringConexao()
       {
            return @"server=DESKTOP-RT242MK\\SQLEXPRESS; database=SistemaFinanceiro12345; Integrated Security=true;User ID=sa;Password=Core@2023;TrustServerCertificate=True";

            //return "Data Source=DESKTOP-RT242MK;Initial Catalog=FINANCEIRO_2023;Integrated Security=True"; // Evitar
       }

    }
}


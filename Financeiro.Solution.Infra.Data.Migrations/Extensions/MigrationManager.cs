
using Financeiro.Solution.Infra.Data.Migrations.Migrations;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host, IConfiguration configuration)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    string connectionString = configuration.GetConnectionString("SqlConnection");
                    string databaseName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;
                    //databaseService.CreateDatabase("SistemaFinanceiro12345");
                    databaseService.CreateDatabase(databaseName);

                    migrationService.ListMigrations();
                    migrationService.MigrateUp(20230426);
                    migrationService.MigrateUp(20231223);
                    migrationService.MigrateUp(20230428);
                    migrationService.MigrateUp(20230429);
                    migrationService.MigrateUp(2023051401);
                    migrationService.MigrateUp(2023051402);
                    migrationService.MigrateUp(2023051403);
                    migrationService.MigrateUp(2023051405);
                    migrationService.MigrateUp(2023051406);
                    migrationService.MigrateUp(2023051407);
                    migrationService.MigrateUp(20231222);
                  


                    migrationService.ListMigrations();

                }
                catch
                {
                    //log errors or ...
                    throw;
                }
            }

            return host;
        }
    }
}

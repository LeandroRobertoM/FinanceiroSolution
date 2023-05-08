
using Financeiro.Solution.Infra.Data.Migrations.Migrations;
using FluentMigrator.Runner;
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
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    databaseService.CreateDatabase("SistemaFinanceiro");


                    migrationService.ListMigrations();
                    migrationService.MigrateUp(20230426);
                    migrationService.MigrateUp(20230427);
                    migrationService.MigrateUp(20230428);
                    migrationService.MigrateUp(20230429);
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

using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Migrations.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations
{
    public class Startup
    {
        //teste Migrations

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddSingleton<Database>();
            services.AddLogging(c => c.AddFluentMigratorConsole())
                    .AddFluentMigratorCore()
                    .ConfigureRunner(c => c.AddSqlServer2012()
                        .WithGlobalConnectionString(Configuration.GetConnectionString("SqlConnection"))
                        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                              .AddLogging(lb => lb.AddFluentMigratorConsole())
                            // Build the service provider
                            .BuildServiceProvider(false);
        }
    }
}


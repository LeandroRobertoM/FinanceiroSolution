using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(20250102)]
    public class InitialSeed_20250102_AddNewColumnsToSistemaFinanceiro : Migration
    {
        public override void Down()
        {
            Delete.Table("SistemaFinanceiro");


        }

        public override void Up()
        {
              Alter.Table("SistemaFinanceiro")
              .AddColumn("DataCriacao").AsDateTime().Nullable()
              .AddColumn("AnoBase").AsDateTime().Nullable()
              .AddColumn("MetaAnual").AsDecimal(18, 2).Nullable()
              .AddColumn("Ativo").AsBoolean().WithDefaultValue(true);

        }
    }
}

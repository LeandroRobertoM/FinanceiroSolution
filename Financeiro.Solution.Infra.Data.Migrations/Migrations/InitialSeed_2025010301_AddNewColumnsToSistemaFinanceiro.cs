using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(2025010301)]
    public class InitialSeed_2025010301_AddNewColumnsToSistemaFinanceiro : Migration
    {
        public override void Down()
        {
            // Remover colunas do sistema financeiro
            Delete.Column("DataCadastro").FromTable("SistemaFinanceiro");
            Delete.Column("AnoBase").FromTable("SistemaFinanceiro");
            Delete.Column("MetaAnual").FromTable("SistemaFinanceiro");
            Delete.Column("Ativo").FromTable("SistemaFinanceiro");
        }

        public override void Up()
        {
            // Alterar a tabela SistemaFinanceiro para adicionar novas colunas
            Alter.Table("SistemaFinanceiro")
                .AddColumn("DataCadastro").AsDateTime().Nullable()
                .AddColumn("AnoBase").AsDateTime().Nullable()
                .AddColumn("MetaAnual").AsDecimal(18, 2).Nullable()
                .AddColumn("Ativo").AsBoolean().WithDefaultValue(true);   
        }
    }
}
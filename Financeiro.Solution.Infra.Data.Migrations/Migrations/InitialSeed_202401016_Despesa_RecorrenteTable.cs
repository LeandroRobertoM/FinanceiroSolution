using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{


    [Migration(202401016)]
    public class InitialSeed_202401016_Despesa_RecorrenteTable : Migration
    {
        public override void Down()
        {
            Delete.Table("DespesaRecorrente");
        }

        public override void Up()
        {
            Create.Table("DespesaRecorrente")
                .WithColumn("IdRecorrente").AsInt32().Identity().PrimaryKey()
                .WithColumn("DespesaId").AsInt32().NotNullable().ForeignKey("Despesa", "IdDespesa")
                .WithColumn("ValorRecorrente").AsDecimal().NotNullable()
                .WithColumn("DataVencimento").AsDateTime().NotNullable()
                .WithColumn("DataCriacao").AsDateTime().NotNullable()
                .WithColumn("Frequencia").AsString().NotNullable()
                .WithColumn("NumeroParcelas").AsInt32().NotNullable()
                .WithColumn("Notificacao").AsBoolean().NotNullable()
                .WithColumn("Status").AsString().NotNullable();
                 Create.Index("IX_DespesaRecorrente_DespesaId").OnTable("DespesaRecorrente").OnColumn("DespesaId").Ascending();

           
        }
    }
}
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{

    [Migration(20240610)]
    public class InitialSeed_20240610_PagamentoTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Pagamento");
        }

        public override void Up()
        {
            Create.Table("Pagamento")
                .WithColumn("IdPagamento").AsInt32().Identity().PrimaryKey()
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("Valor").AsDecimal().NotNullable()
                .WithColumn("TipoPagamento").AsInt32().NotNullable()
                .WithColumn("DataCadastro").AsDateTime().NotNullable()
                .WithColumn("DataPagamento").AsDateTime().NotNullable()
                .WithColumn("DespesaAtrasada").AsBoolean().NotNullable()
                .WithColumn("Despesa_IdDespesa").AsInt32().ForeignKey("Despesa", "IdDespesa");

            // Criar índice para a chave estrangeira
            Create.Index("IX_Pagamento_Despesa_IdDespesa").OnTable("Pagamento").OnColumn("Despesa_IdDespesa").Ascending();
        }
    }
}


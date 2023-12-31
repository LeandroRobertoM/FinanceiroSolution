using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(20230427)]
    public class InitialSeed_20231223_DespesaTable : Migration
    {

        public override void Down()
        {
            Delete.Table("Despesa");

        }

        public override void Up()
        {
            Create.Table("Despesa")
                .WithColumn("IdDespesa").AsInt32().Identity().PrimaryKey()
                .WithColumn("Nome").AsString(50).NotNullable()
                .WithColumn("Valor").AsDecimal().NotNullable()
                .WithColumn("Mes").AsInt32().NotNullable()
                .WithColumn("Ano").AsInt32().NotNullable()
                .WithColumn("TipoDespesa").AsInt16().NotNullable()
                .WithColumn("DataCadastro").AsDateTime().NotNullable()
                .WithColumn("DataAlteracao").AsDateTime()
                .WithColumn("DataPagamento").AsDateTime().Nullable()
                .WithColumn("DataVencimento").AsDateTime().NotNullable()
                .WithColumn("Pago").AsBoolean().NotNullable()
                .WithColumn("DespesaAtrasada").AsBoolean().NotNullable()
                .WithColumn("categoriaId").AsInt32().NotNullable().ForeignKey("Categoria", "IdCategoria");
        }
    }
}

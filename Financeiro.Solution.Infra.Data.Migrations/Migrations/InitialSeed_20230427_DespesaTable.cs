using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(20230427)]
    public class InitialSeed_20230427_DespesaTable : Migration
    {

        public override void Down()
        {
            Delete.Table("Despesa");

        }

        public override void Up()
        {
            Create.Table("Despesa")
                .WithColumn("IdDespesaw").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("IdUser").AsGuid().NotNullable()
                .WithColumn("Valor").AsDecimal().NotNullable()
                .WithColumn("Mes").AsInt32().NotNullable()
                .WithColumn("Ano").AsInt32().NotNullable()
                .WithColumn("TipoDespesa").AsInt16().NotNullable()
                .WithColumn("DataCadastro").AsDateTime().NotNullable()
                .WithColumn("DataPagamento").AsDateTime().NotNullable()
                .WithColumn("DataVencimento").AsDateTime().NotNullable()
                .WithColumn("Pago").AsBoolean().NotNullable()
                .WithColumn("DespesaAtrasada").AsBoolean().NotNullable()
                .WithColumn("categoriaId").AsGuid().NotNullable().ForeignKey("Categoria", "IdCategoria");
        }
    }
}

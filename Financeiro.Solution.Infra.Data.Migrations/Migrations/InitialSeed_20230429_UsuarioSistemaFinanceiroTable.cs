using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{

    [Migration(20230429)]
    public class InitialSeed_20230429_UsuarioSistemaFinanceiroTable : Migration
    {
        public override void Down()
        {
            Delete.Table("UsuarioSistemaFinanceiro");

        }

        public override void Up()
        {
            Create.Table("UsuarioSistemaFinanceiro")
                .WithColumn("IdSistema").AsInt32().NotNullable()
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("EmailUsuario").AsString().NotNullable()
                .WithColumn("Administrador").AsBoolean().NotNullable()
                .WithColumn("SistemaAtual").AsBoolean().NotNullable();

            Create.ForeignKey("FK_UsuarioSistemaFinanceiro_SistemaFinanceiro_IdSistema")
                .FromTable("UsuarioSistemaFinanceiro").ForeignColumn("IdSistema")
                .ToTable("SistemaFinanceiro").PrimaryColumn("Id")
                .OnDeleteOrUpdate(Rule.Cascade);
        }

    }
}

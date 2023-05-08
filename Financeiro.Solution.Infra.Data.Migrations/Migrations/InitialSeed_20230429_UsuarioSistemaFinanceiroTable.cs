using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    public class InitialSeed_20230429_UsuarioSistemaFinanceiroTable : Migration
    {
        public override void Down()
        {
            Delete.Table("UsuarioSistemaFinanceiro");

        }

        public override void Up()
        {
            Create.Table("UsuarioSistemaFinanceiro")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("EmailUsuario").AsInt16().NotNullable()
                .WithColumn("Administrador").AsDateTime().NotNullable();
        }

    }
}

using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(20240428)]
    public class InitialSeed_20240428_UsuarioCreateTable : Migration
    {

        public override void Down()
        {
            Delete.Table("UsuarioCriador");

        }

        public override void Up()
        {
            Create.Table("UsuarioCreate")
           .WithColumn("Id").AsInt32().Identity().PrimaryKey()
           .WithColumn("DataCadastro").AsDateTime().NotNullable()
           .WithColumn("UsuarioCriadorId").AsString(450).NotNullable().ForeignKey("AspNetUsers", "Id")
           .WithColumn("UsuarioCriadoId").AsString(450).NotNullable().ForeignKey("AspNetUsers", "Id");

        }
    }
}

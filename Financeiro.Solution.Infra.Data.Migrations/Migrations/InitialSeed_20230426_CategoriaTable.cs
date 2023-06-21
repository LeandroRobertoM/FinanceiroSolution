using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{

    [Migration(20230426)]
    public class InitialSeed_20230426_CategoriaTable : Migration
    {

        public override void Down()
        {
            Delete.Table("Categoria");
         
        }

        public override void Up()
        {
                 Create.Table("Categoria")
                .WithColumn("IdCategoria").AsInt32().Identity().PrimaryKey()
                .WithColumn("Nome").AsString(50).NotNullable();
        }
    }
}


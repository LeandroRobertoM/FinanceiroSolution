using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{

    [Migration(20230428)]
    public class InitialSeed_20230428_SistemaFinanceiroTable : Migration
    {

        public override void Down()
        {
            Delete.Table("SistemaFinanceiro");
          

        }

        public override void Up()
        {
            Create.Table("SistemaFinanceiro")
       .WithColumn("Id").AsInt32().Identity().PrimaryKey()
       .WithColumn("Mes").AsInt32().NotNullable()
       .WithColumn("Ano").AsInt32().NotNullable()
       .WithColumn("DiaFechamento").AsInt32().NotNullable()
       .WithColumn("GerarCopiaDespesa").AsBoolean().NotNullable()
       .WithColumn("MesCopia").AsInt32().NotNullable()
       .WithColumn("AnoCopia").AsInt32().NotNullable()
       .WithColumn("Nome").AsString().NotNullable();

        }
    }
}
  





using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(2023051401)]
    public class InitialSeed_2023051401_AspNetRolesTable : Migration
    {
        public override void Down()
        {
            Delete.Table("AspNetRoles");
        }

        public override void Up()
        {
            Create.Table("AspNetRoles")
                .WithColumn("Id").AsString(450).NotNullable()
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("NormalizedName").AsString(256).NotNullable()
                .WithColumn("ConcurrencyStamp").AsString().NotNullable();


            Create.PrimaryKey("PK_AspNetRoles")
                .OnTable("AspNetRoles")
                .Columns("Id");

        }
    }
}
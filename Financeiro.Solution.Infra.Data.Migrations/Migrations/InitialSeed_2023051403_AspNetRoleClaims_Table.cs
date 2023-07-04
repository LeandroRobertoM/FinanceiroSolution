using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(2023051403)]
    public  class InitialSeed_2023051403_AspNetRoleClaims_Table : Migration
    {
        public override void Down() {

            Delete.Table("AspNetRoleClaims");

        }

        public override void Up()
        {
            Create.Table("AspNetRoleClaims")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("RoleId").AsString(450)
                .WithColumn("ClaimType").AsString()
                .WithColumn("ClaimValue").AsString();

                 Create.ForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId")
                .FromTable("AspNetRoleClaims").ForeignColumn("RoleId")
                .ToTable("AspNetRoles").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }
    }
}
 
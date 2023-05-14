using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(20230514)]
    public class InitialSeed_20230514_AspNetUserClaims_Table : Migration
    {
        public override void Down()
        {

            Delete.Table("AspNetUserClaims");

        }

        public override void Up()
        {
            Create.Table("AspNetUserClaims")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("RoleId").AsString(450)
                .WithColumn("ClaimType").AsString()
                .WithColumn("ClaimValue").AsString();

            Create.ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId")
           .FromTable("AspNetUsers").ForeignColumn("Id")
           .ToTable("AspNetRoles").PrimaryColumn("Id")
           .OnDelete(Rule.Cascade);
        }
    }
}


using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(2023051404)]
    public class InitialSeed_2023051404_AspNetUserClaims_Table : Migration
    {
        public override void Down()
    {
        Delete.Table("AspNetUserClaims");
    }
         public override void Up()
        {
            Create.Table("AspNetUserClaims")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserId").AsString(450).NotNullable()
                .WithColumn("ClaimType").AsString().Nullable()
                .WithColumn("ClaimValue").AsString().Nullable();

            Create.ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId")
                .FromTable("AspNetUserClaims").ForeignColumn("UserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }
    }
}


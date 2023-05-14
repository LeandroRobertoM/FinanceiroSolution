using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(2023051407)]
    public class InitialSeed_2023051407_AspNetUserTokens_Table : Migration
    {
        public override void Down()
        {
            Delete.Table("AspNetUserTokens");
        }

        public override void Up()
        {
            Create.Table("AspNetUserTokens")
                .WithColumn("UserId").AsString(450).NotNullable()
                .WithColumn("LoginProvider").AsString(128).NotNullable()
                .WithColumn("Name").AsString(128).NotNullable()
                .WithColumn("Value").AsString().Nullable();

            Create.PrimaryKey("PK_AspNetUserTokens")
                .OnTable("AspNetUserTokens")
                .Columns("UserId", "LoginProvider", "Name");

            Create.ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId")
                .FromTable("AspNetUserTokens").ForeignColumn("UserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }
    }
}
 

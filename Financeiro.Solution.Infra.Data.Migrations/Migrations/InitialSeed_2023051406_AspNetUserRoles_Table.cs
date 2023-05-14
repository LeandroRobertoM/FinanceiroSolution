using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(2023051406)]
    public class InitialSeed_2023051406_AspNetUserRoles_Table : Migration
    {
        public override void Down()
        {
             Delete.Table("AspNetUserRoles");
        }

        public override void Up()
        {
            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsString(450).NotNullable()
                .WithColumn("RoleId").AsString(450).NotNullable();

            Create.PrimaryKey("PK_AspNetUserRoles")
                .OnTable("AspNetUserRoles")
                .Columns("UserId", "RoleId");

            Create.ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId")
                .FromTable("AspNetUserRoles").ForeignColumn("UserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);

            Create.ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId")
                .FromTable("AspNetUserRoles").ForeignColumn("RoleId")
                .ToTable("AspNetRoles").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }
    }
}



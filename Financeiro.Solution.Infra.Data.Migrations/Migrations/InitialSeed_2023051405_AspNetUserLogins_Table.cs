using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Solution.Infra.Data.Migrations.Migrations
{
    [Migration(2023051405)]
    public class InitialSeed_2023051405_AspNetUserLogins_Table : Migration
    {
        public override void Down()
        {
            Delete.Table("AspNetUserLogins");
        }

        public override void Up()
        {
            Create.Table("AspNetUserLogins")
                .WithColumn("LoginProvider").AsString(128).NotNullable()
                .WithColumn("ProviderKey").AsString(128).NotNullable()
                .WithColumn("ProviderDisplayName").AsString().Nullable()
                .WithColumn("UserId").AsString(450).NotNullable();

            Create.PrimaryKey("PK_AspNetUserLogins")
                .OnTable("AspNetUserLogins")
                .Columns("LoginProvider", "ProviderKey");

            Create.ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId")
                .FromTable("AspNetUserLogins").ForeignColumn("UserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }
    }
}


using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashRolls.Data.Migrations
{
    public partial class Add_ContactMessages_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Sender = table.Column<string>(maxLength: 250, nullable: false),
                    Message = table.Column<string>(maxLength: 450, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModiefiedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Information = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");
        }
    }
}

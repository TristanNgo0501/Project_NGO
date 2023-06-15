using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_NGO.Migrations
{
    public partial class editContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Contact_Form",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contact_Form");
        }
    }
}

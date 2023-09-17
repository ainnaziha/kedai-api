using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KedaiAPI.Migrations
{
    public partial class EnableNullInOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Orders",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Total",
                table: "Orders",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KedaiAPI.Migrations
{
    public partial class EnableNullNameInOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
               name: "Name",
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

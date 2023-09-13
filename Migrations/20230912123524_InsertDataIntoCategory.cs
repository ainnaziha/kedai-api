using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KedaiAPI.Migrations
{
    public partial class InsertDataIntoCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "CategoryId", "CategoryName" },
            values: new object[,]
            {
                { 1, "Laptops" },
                { 2, "Smartphones" },
                { 3, "Tablets" },
                { 4, "Accessories" },
                { 5, "Gaming" },
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "Categories",
               keyColumn: "CategoryId",
               keyValues: new object[] { 1, 2, 3, 4, 5 });
        }
    }
}

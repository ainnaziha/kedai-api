using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KedaiAPI.Migrations
{
    public partial class InsertDataIntoProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Description", "Image", "Price", "CategoryId" },
                values: new object[,]
                {
                    { 1, "Laptop 1", "Description for Laptop 1", "uploads/1.jpg", 999.99, 1 },
                    { 2, "Laptop 2", "Description for Laptop 2", "uploads/2.jpg", 1099.99, 1 },
                    { 3, "Laptop 3", "Description for Laptop 3", "uploads/3.jpg", 899.99, 1 },

                    { 4, "Phone 1", "Description for Phone 1", "uploads/4.jpg", 499.99, 2 },
                    { 5, "Phone 2", "Description for Phone 2", "uploads/5.jpg", 599.99, 2 },
                    { 6, "Phone 3", "Description for Phone 3", "uploads/6.jpg", 699.99, 2 },

                    { 7, "Tablet 1", "Description for Tablet 1", "uploads/7.jpg", 299.99, 3 },
                    { 8, "Tablet 2", "Description for Tablet 2", "uploads/8.jpg", 399.99, 3 },
                    { 9, "Tablet 3", "Description for Tablet 3", "uploads/9.jpg", 499.99, 3 },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }
    }
}

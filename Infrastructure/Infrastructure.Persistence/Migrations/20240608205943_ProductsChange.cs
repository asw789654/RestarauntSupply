using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProductsChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_StorageTypes_StorageTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StorageTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StorageTypeId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageTypeId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StorageTypeId",
                table: "Products",
                column: "StorageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StorageTypes_StorageTypeId",
                table: "Products",
                column: "StorageTypeId",
                principalTable: "StorageTypes",
                principalColumn: "StorageTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

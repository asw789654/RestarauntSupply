using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StorageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Storages_StorageId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Storages_StorageId",
                table: "Products",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Storages_StorageId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Storages_StorageId",
                table: "Products",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId");
        }
    }
}

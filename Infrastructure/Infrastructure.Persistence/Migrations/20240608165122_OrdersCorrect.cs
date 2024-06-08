using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrdersCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ApplicationUsers_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Order_OrderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_StorageType_StorageTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Storage_StorageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_StorageType_StorageTypeId",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageType",
                table: "StorageType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storage",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "StorageType",
                newName: "StorageTypes");

            migrationBuilder.RenameTable(
                name: "Storage",
                newName: "Storages");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Storage_StorageTypeId",
                table: "Storages",
                newName: "IX_Storages_StorageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageTypes",
                table: "StorageTypes",
                column: "StorageTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storages",
                table: "Storages",
                column: "StorageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ApplicationUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StorageTypes_StorageTypeId",
                table: "Products",
                column: "StorageTypeId",
                principalTable: "StorageTypes",
                principalColumn: "StorageTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Storages_StorageId",
                table: "Products",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_StorageTypes_StorageTypeId",
                table: "Storages",
                column: "StorageTypeId",
                principalTable: "StorageTypes",
                principalColumn: "StorageTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ApplicationUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_StorageTypes_StorageTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Storages_StorageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_StorageTypes_StorageTypeId",
                table: "Storages");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageTypes",
                table: "StorageTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storages",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "StorageTypes",
                newName: "StorageType");

            migrationBuilder.RenameTable(
                name: "Storages",
                newName: "Storage");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Storages_StorageTypeId",
                table: "Storage",
                newName: "IX_Storage_StorageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageType",
                table: "StorageType",
                column: "StorageTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storage",
                table: "Storage",
                column: "StorageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ApplicationUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "ApplicationUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Order_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StorageType_StorageTypeId",
                table: "Products",
                column: "StorageTypeId",
                principalTable: "StorageType",
                principalColumn: "StorageTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Storage_StorageId",
                table: "Products",
                column: "StorageId",
                principalTable: "Storage",
                principalColumn: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_StorageType_StorageTypeId",
                table: "Storage",
                column: "StorageTypeId",
                principalTable: "StorageType",
                principalColumn: "StorageTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

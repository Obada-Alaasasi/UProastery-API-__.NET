using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UProastery.Migrations
{
    /// <inheritdoc />
    public partial class modify_models_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "085f1de3-af1c-498c-840e-3e530d14650d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "367fee09-cdd6-4d21-8b03-12dd31f8318c");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30ae8292-56ea-4a5b-b696-be0fbf0d11b3", null, "Administrator", "ADMINISTRATOR" },
                    { "93c0b8c7-298a-4d9f-a145-6c33395045df", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Added",
                value: new DateTime(2023, 5, 22, 15, 4, 5, 467, DateTimeKind.Local).AddTicks(5556));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Added", "Stock" },
                values: new object[] { new DateTime(2023, 5, 22, 15, 4, 5, 467, DateTimeKind.Local).AddTicks(5576), 30 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "Added",
                value: new DateTime(2023, 5, 22, 15, 4, 5, 467, DateTimeKind.Local).AddTicks(5580));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Added", "Stock" },
                values: new object[] { new DateTime(2023, 5, 22, 15, 4, 5, 467, DateTimeKind.Local).AddTicks(5583), 30 });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30ae8292-56ea-4a5b-b696-be0fbf0d11b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c0b8c7-298a-4d9f-a145-6c33395045df");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "085f1de3-af1c-498c-840e-3e530d14650d", null, "Customer", "CUSTOMER" },
                    { "367fee09-cdd6-4d21-8b03-12dd31f8318c", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Added",
                value: new DateTime(2023, 5, 21, 1, 55, 53, 494, DateTimeKind.Local).AddTicks(767));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Added", "Stock" },
                values: new object[] { new DateTime(2023, 5, 21, 1, 55, 53, 494, DateTimeKind.Local).AddTicks(798), null });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "Added",
                value: new DateTime(2023, 5, 21, 1, 55, 53, 494, DateTimeKind.Local).AddTicks(801));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Added", "Stock" },
                values: new object[] { new DateTime(2023, 5, 21, 1, 55, 53, 494, DateTimeKind.Local).AddTicks(804), null });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

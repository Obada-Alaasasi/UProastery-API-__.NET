using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UProastery.Migrations
{
    /// <inheritdoc />
    public partial class remove_IdentityCoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Customer", "CUSTOMER" },
                    { 2, null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Added",
                value: new DateTime(2023, 5, 25, 15, 18, 20, 583, DateTimeKind.Local).AddTicks(3370));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "Added",
                value: new DateTime(2023, 5, 25, 15, 18, 20, 583, DateTimeKind.Local).AddTicks(3393));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "Added",
                value: new DateTime(2023, 5, 25, 15, 18, 20, 583, DateTimeKind.Local).AddTicks(3398));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "Added",
                value: new DateTime(2023, 5, 25, 15, 18, 20, 583, DateTimeKind.Local).AddTicks(3401));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c1c9aeb-1e18-43b5-9fce-c8f595f3ebac", null, "Customer", "CUSTOMER" },
                    { "4b6c321e-02fd-49de-b652-7a43829d2094", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Added",
                value: new DateTime(2023, 5, 25, 14, 40, 54, 283, DateTimeKind.Local).AddTicks(9462));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "Added",
                value: new DateTime(2023, 5, 25, 14, 40, 54, 283, DateTimeKind.Local).AddTicks(9484));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "Added",
                value: new DateTime(2023, 5, 25, 14, 40, 54, 283, DateTimeKind.Local).AddTicks(9488));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "Added",
                value: new DateTime(2023, 5, 25, 14, 40, 54, 283, DateTimeKind.Local).AddTicks(9490));
        }
    }
}

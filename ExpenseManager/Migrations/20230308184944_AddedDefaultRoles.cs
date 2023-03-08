using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseManager.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27574cde-715f-4940-bd4d-20599cc1f7d6", null, "User", "USER" },
                    { "3871cc4d-c2c0-4ac6-a6f5-326596dd7a29", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 3, 9, 0, 19, 44, 263, DateTimeKind.Local).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 3, 9, 0, 19, 44, 263, DateTimeKind.Local).AddTicks(7538));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2023, 3, 9, 0, 19, 44, 263, DateTimeKind.Local).AddTicks(7539));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27574cde-715f-4940-bd4d-20599cc1f7d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3871cc4d-c2c0-4ac6-a6f5-326596dd7a29");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 3, 9, 0, 0, 52, 723, DateTimeKind.Local).AddTicks(4405));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 3, 9, 0, 0, 52, 723, DateTimeKind.Local).AddTicks(4417));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2023, 3, 9, 0, 0, 52, 723, DateTimeKind.Local).AddTicks(4471));
        }
    }
}

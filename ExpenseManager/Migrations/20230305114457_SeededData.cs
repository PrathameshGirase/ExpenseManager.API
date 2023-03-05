using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseManager.Migrations
{
    /// <inheritdoc />
    public partial class SeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transaction_Type",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "Transaction_TypeId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Transaction_Type_Id",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Food And Beverages" },
                    { 2, "Pantry" },
                    { 3, "Stationary" },
                    { 4, "Travel" },
                    { 5, "Others" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Expense" },
                    { 2, "Income" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "Name", "Transaction_TypeId", "Transaction_Type_Id" },
                values: new object[,]
                {
                    { 1, 50.0, 1, new DateTime(2023, 4, 4, 17, 14, 57, 832, DateTimeKind.Local).AddTicks(6540), "My Treat", "Starbucks", null, 1 },
                    { 2, 100.0, 5, new DateTime(2023, 4, 4, 17, 14, 57, 832, DateTimeKind.Local).AddTicks(6555), "Profit", "Stocks", null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Transaction_TypeId",
                table: "Transactions",
                column: "Transaction_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_Transaction_TypeId",
                table: "Transactions",
                column: "Transaction_TypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_Transaction_TypeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Transaction_TypeId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Transaction_TypeId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Transaction_Type_Id",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "Transaction_Type",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

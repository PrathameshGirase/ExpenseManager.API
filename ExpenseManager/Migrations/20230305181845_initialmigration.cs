using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseManager.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 5, "Staff" },
                    { 6, "Others" }
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
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "Name", "TransactionTypeId" },
                values: new object[,]
                {
                    { 1, 50.0, 1, new DateTime(2023, 3, 5, 23, 48, 44, 956, DateTimeKind.Local).AddTicks(4277), "My Treat", "Starbucks", 1 },
                    { 2, 100.0, 5, new DateTime(2023, 3, 5, 23, 48, 44, 956, DateTimeKind.Local).AddTicks(4286), "Profit", "Stocks", 2 },
                    { 5, 100.0, 5, new DateTime(2023, 3, 5, 23, 48, 44, 956, DateTimeKind.Local).AddTicks(4288), "Null", "Salary", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "TransactionTypes");
        }
    }
}

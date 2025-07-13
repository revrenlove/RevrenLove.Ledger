using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingRecurring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecurringTransactionId",
                table: "LedgerItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecurringTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringTransaction_FinancialAccountHolders_FinancialAccountHolderId",
                        column: x => x.FinancialAccountHolderId,
                        principalTable: "FinancialAccountHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecurringTransaction_FinancialAccounts_FinancialAccountId",
                        column: x => x.FinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LedgerItems_RecurringTransactionId",
                table: "LedgerItems",
                column: "RecurringTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_FinancialAccountHolderId",
                table: "RecurringTransaction",
                column: "FinancialAccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_FinancialAccountId",
                table: "RecurringTransaction",
                column: "FinancialAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerItems_RecurringTransaction_RecurringTransactionId",
                table: "LedgerItems",
                column: "RecurringTransactionId",
                principalTable: "RecurringTransaction",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerItems_RecurringTransaction_RecurringTransactionId",
                table: "LedgerItems");

            migrationBuilder.DropTable(
                name: "RecurringTransaction");

            migrationBuilder.DropIndex(
                name: "IX_LedgerItems_RecurringTransactionId",
                table: "LedgerItems");

            migrationBuilder.DropColumn(
                name: "RecurringTransactionId",
                table: "LedgerItems");
        }
    }
}

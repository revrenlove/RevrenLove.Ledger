using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class FixingConfigForQueryFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerTransactions_FinancialAccounts_FinancialAccountId",
                table: "LedgerTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerTransactions_FinancialAccounts_FinancialAccountId",
                table: "LedgerTransactions",
                column: "FinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerTransactions_FinancialAccounts_FinancialAccountId",
                table: "LedgerTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerTransactions_FinancialAccounts_FinancialAccountId",
                table: "LedgerTransactions",
                column: "FinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

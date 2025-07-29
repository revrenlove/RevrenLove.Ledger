using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FinalizingModelHopefully : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerItems_RecurringTransaction_RecurringTransactionId",
                table: "LedgerItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_FinancialAccountHolders_FinancialAccountHolderId",
                table: "RecurringTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_FinancialAccounts_FinancialAccountId",
                table: "RecurringTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecurringTransaction",
                table: "RecurringTransaction");

            migrationBuilder.RenameTable(
                name: "RecurringTransaction",
                newName: "RecurringTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_FinancialAccountId",
                table: "RecurringTransactions",
                newName: "IX_RecurringTransactions_FinancialAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_FinancialAccountHolderId",
                table: "RecurringTransactions",
                newName: "IX_RecurringTransactions_FinancialAccountHolderId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LedgerItems",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FinancialAccounts",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FinancialAccountHolders",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RecurringTransactions",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecurringTransactions",
                table: "RecurringTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerItems_RecurringTransactions_RecurringTransactionId",
                table: "LedgerItems",
                column: "RecurringTransactionId",
                principalTable: "RecurringTransactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransactions_FinancialAccountHolders_FinancialAccountHolderId",
                table: "RecurringTransactions",
                column: "FinancialAccountHolderId",
                principalTable: "FinancialAccountHolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransactions_FinancialAccounts_FinancialAccountId",
                table: "RecurringTransactions",
                column: "FinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerItems_RecurringTransactions_RecurringTransactionId",
                table: "LedgerItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransactions_FinancialAccountHolders_FinancialAccountHolderId",
                table: "RecurringTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransactions_FinancialAccounts_FinancialAccountId",
                table: "RecurringTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecurringTransactions",
                table: "RecurringTransactions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LedgerItems");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FinancialAccounts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FinancialAccountHolders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RecurringTransactions");

            migrationBuilder.RenameTable(
                name: "RecurringTransactions",
                newName: "RecurringTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransactions_FinancialAccountId",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_FinancialAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransactions_FinancialAccountHolderId",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_FinancialAccountHolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecurringTransaction",
                table: "RecurringTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerItems_RecurringTransaction_RecurringTransactionId",
                table: "LedgerItems",
                column: "RecurringTransactionId",
                principalTable: "RecurringTransaction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_FinancialAccountHolders_FinancialAccountHolderId",
                table: "RecurringTransaction",
                column: "FinancialAccountHolderId",
                principalTable: "FinancialAccountHolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_FinancialAccounts_FinancialAccountId",
                table: "RecurringTransaction",
                column: "FinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

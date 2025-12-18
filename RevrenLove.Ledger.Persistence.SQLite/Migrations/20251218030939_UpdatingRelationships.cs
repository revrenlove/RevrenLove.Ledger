using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinancialAccounts_UserId_Name",
                table: "FinancialAccounts");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "LedgerTransactions",
                newName: "DatePosted");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RecurringTransactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DestinationFinancialAccountId",
                table: "RecurringTransactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEffective",
                table: "ProspectiveTransactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProspectiveTransactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DestinationFinancialAccountId",
                table: "ProspectiveTransactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                table: "LedgerTransactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FriendlyId",
                table: "FinancialAccounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBalanceExempt",
                table: "FinancialAccounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransactions_DestinationFinancialAccountId",
                table: "RecurringTransactions",
                column: "DestinationFinancialAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProspectiveTransactions_DestinationFinancialAccountId",
                table: "ProspectiveTransactions",
                column: "DestinationFinancialAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccounts_UserId_FriendlyId",
                table: "FinancialAccounts",
                columns: new[] { "UserId", "FriendlyId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProspectiveTransactions_FinancialAccounts_DestinationFinancialAccountId",
                table: "ProspectiveTransactions",
                column: "DestinationFinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransactions_FinancialAccounts_DestinationFinancialAccountId",
                table: "RecurringTransactions",
                column: "DestinationFinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProspectiveTransactions_FinancialAccounts_DestinationFinancialAccountId",
                table: "ProspectiveTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransactions_FinancialAccounts_DestinationFinancialAccountId",
                table: "RecurringTransactions");

            migrationBuilder.DropIndex(
                name: "IX_RecurringTransactions_DestinationFinancialAccountId",
                table: "RecurringTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ProspectiveTransactions_DestinationFinancialAccountId",
                table: "ProspectiveTransactions");

            migrationBuilder.DropIndex(
                name: "IX_FinancialAccounts_UserId_FriendlyId",
                table: "FinancialAccounts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RecurringTransactions");

            migrationBuilder.DropColumn(
                name: "DestinationFinancialAccountId",
                table: "RecurringTransactions");

            migrationBuilder.DropColumn(
                name: "DateEffective",
                table: "ProspectiveTransactions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProspectiveTransactions");

            migrationBuilder.DropColumn(
                name: "DestinationFinancialAccountId",
                table: "ProspectiveTransactions");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "LedgerTransactions");

            migrationBuilder.DropColumn(
                name: "FriendlyId",
                table: "FinancialAccounts");

            migrationBuilder.DropColumn(
                name: "IsBalanceExempt",
                table: "FinancialAccounts");

            migrationBuilder.RenameColumn(
                name: "DatePosted",
                table: "LedgerTransactions",
                newName: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccounts_UserId_Name",
                table: "FinancialAccounts",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }
    }
}

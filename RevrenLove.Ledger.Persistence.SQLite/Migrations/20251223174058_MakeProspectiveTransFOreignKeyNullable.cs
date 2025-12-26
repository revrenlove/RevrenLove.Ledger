using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class MakeProspectiveTransFOreignKeyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProspectiveTransactions_FinancialAccounts_DestinationFinancialAccountId",
                table: "ProspectiveTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "DestinationFinancialAccountId",
                table: "ProspectiveTransactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_ProspectiveTransactions_FinancialAccounts_DestinationFinancialAccountId",
                table: "ProspectiveTransactions",
                column: "DestinationFinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProspectiveTransactions_FinancialAccounts_DestinationFinancialAccountId",
                table: "ProspectiveTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "DestinationFinancialAccountId",
                table: "ProspectiveTransactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProspectiveTransactions_FinancialAccounts_DestinationFinancialAccountId",
                table: "ProspectiveTransactions",
                column: "DestinationFinancialAccountId",
                principalTable: "FinancialAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

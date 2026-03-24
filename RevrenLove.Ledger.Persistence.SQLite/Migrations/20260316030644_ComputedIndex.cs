using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class ComputedIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComputedDisplayValue",
                table: "FinancialTransactions",
                type: "TEXT",
                nullable: true,
                computedColumnSql: "strftime('%Y-%m-%d', Date) || '|' || Amount || '|' || Id",
                stored: true);

            migrationBuilder.CreateTable(
                name: "RunningBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FinancialTransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunningBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RunningBalances_FinancialTransactions_FinancialTransactionId",
                        column: x => x.FinancialTransactionId,
                        principalTable: "FinancialTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RunningBalances_FinancialTransactionId",
                table: "RunningBalances",
                column: "FinancialTransactionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RunningBalances");

            migrationBuilder.DropColumn(
                name: "ComputedDisplayValue",
                table: "FinancialTransactions");
        }
    }
}

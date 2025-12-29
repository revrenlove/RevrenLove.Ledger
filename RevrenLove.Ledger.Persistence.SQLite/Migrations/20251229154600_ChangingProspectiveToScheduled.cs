using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class ChangingProspectiveToScheduled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProspectiveTransactions");

            migrationBuilder.CreateTable(
                name: "ScheduledTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FinancialAccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DestinationFinancialAccountId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DateEffective = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledTransactions_FinancialAccounts_DestinationFinancialAccountId",
                        column: x => x.DestinationFinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduledTransactions_FinancialAccounts_FinancialAccountId",
                        column: x => x.FinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTransactions_DestinationFinancialAccountId",
                table: "ScheduledTransactions",
                column: "DestinationFinancialAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTransactions_FinancialAccountId",
                table: "ScheduledTransactions",
                column: "FinancialAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledTransactions");

            migrationBuilder.CreateTable(
                name: "ProspectiveTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DestinationFinancialAccountId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FinancialAccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    DateEffective = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProspectiveTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProspectiveTransactions_FinancialAccounts_DestinationFinancialAccountId",
                        column: x => x.DestinationFinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProspectiveTransactions_FinancialAccounts_FinancialAccountId",
                        column: x => x.FinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProspectiveTransactions_DestinationFinancialAccountId",
                table: "ProspectiveTransactions",
                column: "DestinationFinancialAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProspectiveTransactions_FinancialAccountId",
                table: "ProspectiveTransactions",
                column: "FinancialAccountId");
        }
    }
}

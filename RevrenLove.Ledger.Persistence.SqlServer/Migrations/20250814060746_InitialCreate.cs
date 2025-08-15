using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialAccountHolders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAccountHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialAccountFinancialAccountHolder",
                columns: table => new
                {
                    FinancialAccountHoldersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAccountFinancialAccountHolder", x => new { x.FinancialAccountHoldersId, x.FinancialAccountsId });
                    table.ForeignKey(
                        name: "FK_FinancialAccountFinancialAccountHolder_FinancialAccountHolders_FinancialAccountHoldersId",
                        column: x => x.FinancialAccountHoldersId,
                        principalTable: "FinancialAccountHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialAccountFinancialAccountHolder_FinancialAccounts_FinancialAccountsId",
                        column: x => x.FinancialAccountsId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurringTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringTransactions_FinancialAccountHolders_FinancialAccountHolderId",
                        column: x => x.FinancialAccountHolderId,
                        principalTable: "FinancialAccountHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecurringTransactions_FinancialAccounts_FinancialAccountId",
                        column: x => x.FinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecurringTransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsProjection = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerItems_FinancialAccountHolders_FinancialAccountHolderId",
                        column: x => x.FinancialAccountHolderId,
                        principalTable: "FinancialAccountHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LedgerItems_FinancialAccounts_FinancialAccountId",
                        column: x => x.FinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LedgerItems_RecurringTransactions_RecurringTransactionId",
                        column: x => x.RecurringTransactionId,
                        principalTable: "RecurringTransactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAccountFinancialAccountHolder_FinancialAccountsId",
                table: "FinancialAccountFinancialAccountHolder",
                column: "FinancialAccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerItems_FinancialAccountHolderId",
                table: "LedgerItems",
                column: "FinancialAccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerItems_FinancialAccountId",
                table: "LedgerItems",
                column: "FinancialAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerItems_RecurringTransactionId",
                table: "LedgerItems",
                column: "RecurringTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransactions_FinancialAccountHolderId",
                table: "RecurringTransactions",
                column: "FinancialAccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransactions_FinancialAccountId",
                table: "RecurringTransactions",
                column: "FinancialAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialAccountFinancialAccountHolder");

            migrationBuilder.DropTable(
                name: "LedgerItems");

            migrationBuilder.DropTable(
                name: "RecurringTransactions");

            migrationBuilder.DropTable(
                name: "FinancialAccountHolders");

            migrationBuilder.DropTable(
                name: "FinancialAccounts");
        }
    }
}

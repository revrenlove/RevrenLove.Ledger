using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.Migrations
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "LedgerItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialAccountHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", rowVersion: true, nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialAccountFinancialAccountHolder");

            migrationBuilder.DropTable(
                name: "LedgerItems");

            migrationBuilder.DropTable(
                name: "FinancialAccountHolders");

            migrationBuilder.DropTable(
                name: "FinancialAccounts");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class RemovingComputedDisplayName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComputedDisplayValue",
                table: "FinancialTransactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComputedDisplayValue",
                table: "FinancialTransactions",
                type: "TEXT",
                nullable: false,
                computedColumnSql: "strftime('%Y-%m-%d', Date) || '|' || Amount || '|' || Id",
                stored: true);
        }
    }
}

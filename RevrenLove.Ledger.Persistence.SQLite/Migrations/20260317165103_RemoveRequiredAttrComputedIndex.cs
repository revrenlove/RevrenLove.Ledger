using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevrenLove.Ledger.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredAttrComputedIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ComputedDisplayValue",
                table: "FinancialTransactions",
                type: "TEXT",
                nullable: false,
                computedColumnSql: "strftime('%Y-%m-%d', Date) || '|' || Amount || '|' || Id",
                stored: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true,
                oldComputedColumnSql: "strftime('%Y-%m-%d', Date) || '|' || Amount || '|' || Id",
                oldStored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ComputedDisplayValue",
                table: "FinancialTransactions",
                type: "TEXT",
                nullable: true,
                computedColumnSql: "strftime('%Y-%m-%d', Date) || '|' || Amount || '|' || Id",
                stored: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldComputedColumnSql: "strftime('%Y-%m-%d', Date) || '|' || Amount || '|' || Id",
                oldStored: true);
        }
    }
}

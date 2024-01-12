using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReimbursementClaim.Migrations
{
    /// <inheritdoc />
    public partial class Sample1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "detail",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "detail");
        }
    }
}

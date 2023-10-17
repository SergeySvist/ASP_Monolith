using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCoreBasics.Migrations
{
    /// <inheritdoc />
    public partial class AddedDetailsTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Transactions");
        }
    }
}

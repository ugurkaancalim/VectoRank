using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSE.App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class appdataid_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppDataId",
                table: "Apps",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppDataId",
                table: "Apps");
        }
    }
}

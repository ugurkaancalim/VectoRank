using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSE.App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _23947 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppDataId",
                table: "Apps",
                newName: "DataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataId",
                table: "Apps",
                newName: "AppDataId");
        }
    }
}

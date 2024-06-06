using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SSE.App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class datasourceremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDataSources");

            migrationBuilder.DropTable(
                name: "DataSourceAddresses");

            migrationBuilder.DropTable(
                name: "DataSources");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDataSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataSourceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDataSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSourceAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataSourceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DatabaseSize = table.Column<int>(type: "integer", nullable: false, defaultValue: 100),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PartitionSize = table.Column<int>(type: "integer", nullable: false, defaultValue: 250),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDataSources_AppId",
                table: "AppDataSources",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDataSources_DataSourceId",
                table: "AppDataSources",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_Id",
                table: "DataSources",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_UserId",
                table: "DataSources",
                column: "UserId");
        }
    }
}

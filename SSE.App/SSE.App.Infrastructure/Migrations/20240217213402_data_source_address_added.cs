using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SSE.App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class data_source_address_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineSourceId",
                table: "DataSources");

            migrationBuilder.AddColumn<int>(
                name: "DatabaseSize",
                table: "DataSources",
                type: "integer",
                nullable: false,
                defaultValue: 100);

            migrationBuilder.AddColumn<int>(
                name: "PartitionSize",
                table: "DataSources",
                type: "integer",
                nullable: false,
                defaultValue: 250);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSourceAddresses");

            migrationBuilder.DropColumn(
                name: "DatabaseSize",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "PartitionSize",
                table: "DataSources");

            migrationBuilder.AddColumn<string>(
                name: "EngineSourceId",
                table: "DataSources",
                type: "text",
                nullable: true);
        }
    }
}

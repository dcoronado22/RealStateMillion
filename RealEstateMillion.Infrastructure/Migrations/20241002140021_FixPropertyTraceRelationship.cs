using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateMillion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPropertyTraceRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    IdOwner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.IdOwner);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    IdProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CodeInternal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    IdOwner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.IdProperty);
                    table.ForeignKey(
                        name: "FK_Properties_Owners_IdOwner",
                        column: x => x.IdOwner,
                        principalTable: "Owners",
                        principalColumn: "IdOwner",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    IdPropertyImage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    File = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.IdPropertyImage);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_IdProperty",
                        column: x => x.IdProperty,
                        principalTable: "Properties",
                        principalColumn: "IdProperty",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTraces",
                columns: table => new
                {
                    IdPropertyTrace = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTraces", x => x.IdPropertyTrace);
                    table.ForeignKey(
                        name: "FK_PropertyTraces_Properties_IdProperty",
                        column: x => x.IdProperty,
                        principalTable: "Properties",
                        principalColumn: "IdProperty",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CodeInternal",
                table: "Properties",
                column: "CodeInternal",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_IdOwner",
                table: "Properties",
                column: "IdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_IdProperty",
                table: "PropertyImages",
                column: "IdProperty");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTraces_IdProperty",
                table: "PropertyTraces",
                column: "IdProperty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "PropertyTraces");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}

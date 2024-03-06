using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleItTest.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectQuotes",
                columns: table => new
                {
                    QuoteSentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salesperson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerState = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    MarketingCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumberOfQuotes = table.Column<int>(type: "int", nullable: false),
                    TotalNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectQuotes");
        }
    }
}

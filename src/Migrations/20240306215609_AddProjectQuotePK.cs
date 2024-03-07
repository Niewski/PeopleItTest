using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleItTest.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectQuotePK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectQuoteId",
                table: "ProjectQuotes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectQuotes",
                table: "ProjectQuotes",
                column: "ProjectQuoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectQuotes",
                table: "ProjectQuotes");

            migrationBuilder.DropColumn(
                name: "ProjectQuoteId",
                table: "ProjectQuotes");
        }
    }
}

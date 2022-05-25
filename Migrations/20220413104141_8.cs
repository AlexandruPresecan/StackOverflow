using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreationDate",
                table: "Questions",
                column: "CreationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_CreationDate",
                table: "Questions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_CreationDate",
                table: "Questions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreationDate",
                table: "Questions",
                column: "CreationDate");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "Answer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "Answer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

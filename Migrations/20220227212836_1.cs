using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTag_Questions_QuestionId",
                table: "QuestionTag");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTag_Tag_TagId",
                table: "QuestionTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTag",
                table: "QuestionTag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "QuestionTag",
                newName: "QuestionTags");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionTag_TagId",
                table: "QuestionTags",
                newName: "IX_QuestionTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionTag_QuestionId",
                table: "QuestionTags",
                newName: "IX_QuestionTags_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Questions_QuestionId",
                table: "QuestionTags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Questions_QuestionId",
                table: "QuestionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "QuestionTags",
                newName: "QuestionTag");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionTags_TagId",
                table: "QuestionTag",
                newName: "IX_QuestionTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionTags_QuestionId",
                table: "QuestionTag",
                newName: "IX_QuestionTag_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionTag",
                table: "QuestionTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTag_Questions_QuestionId",
                table: "QuestionTag",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTag_Tag_TagId",
                table: "QuestionTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

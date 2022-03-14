using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverflow.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_AspNetUsers_AuthorId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVote_Answer_AnswerId",
                table: "AnswerVote");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVote_AspNetUsers_AuthorId",
                table: "AnswerVote");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionVote_AspNetUsers_AuthorId",
                table: "QuestionVote");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionVote_Questions_QuestionId",
                table: "QuestionVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionVote",
                table: "QuestionVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerVote",
                table: "AnswerVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "QuestionVote",
                newName: "QuestionVotes");

            migrationBuilder.RenameTable(
                name: "AnswerVote",
                newName: "AnswerVotes");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionVote_QuestionId",
                table: "QuestionVotes",
                newName: "IX_QuestionVotes_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionVote_AuthorId",
                table: "QuestionVotes",
                newName: "IX_QuestionVotes_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerVote_AuthorId",
                table: "AnswerVotes",
                newName: "IX_AnswerVotes_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerVote_AnswerId",
                table: "AnswerVotes",
                newName: "IX_AnswerVotes_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_AuthorId",
                table: "Answers",
                newName: "IX_Answers_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionVotes",
                table: "QuestionVotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerVotes",
                table: "AnswerVotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_AuthorId",
                table: "Answers",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVotes_Answers_AnswerId",
                table: "AnswerVotes",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVotes_AspNetUsers_AuthorId",
                table: "AnswerVotes",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionVotes_AspNetUsers_AuthorId",
                table: "QuestionVotes",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionVotes_Questions_QuestionId",
                table: "QuestionVotes",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_AuthorId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVotes_Answers_AnswerId",
                table: "AnswerVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVotes_AspNetUsers_AuthorId",
                table: "AnswerVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionVotes_AspNetUsers_AuthorId",
                table: "QuestionVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionVotes_Questions_QuestionId",
                table: "QuestionVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionVotes",
                table: "QuestionVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerVotes",
                table: "AnswerVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "QuestionVotes",
                newName: "QuestionVote");

            migrationBuilder.RenameTable(
                name: "AnswerVotes",
                newName: "AnswerVote");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionVotes_QuestionId",
                table: "QuestionVote",
                newName: "IX_QuestionVote_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionVotes_AuthorId",
                table: "QuestionVote",
                newName: "IX_QuestionVote_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerVotes_AuthorId",
                table: "AnswerVote",
                newName: "IX_AnswerVote_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerVotes_AnswerId",
                table: "AnswerVote",
                newName: "IX_AnswerVote_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_AuthorId",
                table: "Answer",
                newName: "IX_Answer_AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionVote",
                table: "QuestionVote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerVote",
                table: "AnswerVote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_AspNetUsers_AuthorId",
                table: "Answer",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVote_Answer_AnswerId",
                table: "AnswerVote",
                column: "AnswerId",
                principalTable: "Answer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVote_AspNetUsers_AuthorId",
                table: "AnswerVote",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionVote_AspNetUsers_AuthorId",
                table: "QuestionVote",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionVote_Questions_QuestionId",
                table: "QuestionVote",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

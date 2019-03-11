using Microsoft.EntityFrameworkCore.Migrations;

namespace Georest.Domain.Migrations
{
    public partial class Migration1_AddedModelsAndAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentResponses_StudentLabs_StudentLabId",
                table: "StudentResponses");

            migrationBuilder.DropTable(
                name: "StudentLabs");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "StudentResponses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Lab",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "InstructorResponses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "InstructorResponses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lab_StudentId",
                table: "Lab",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorResponses_ExerciseId",
                table: "InstructorResponses",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorResponses_Exercises_ExerciseId",
                table: "InstructorResponses",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lab_Students_StudentId",
                table: "Lab",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentResponses_Lab_StudentLabId",
                table: "StudentResponses",
                column: "StudentLabId",
                principalTable: "Lab",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructorResponses_Exercises_ExerciseId",
                table: "InstructorResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lab_Students_StudentId",
                table: "Lab");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentResponses_Lab_StudentLabId",
                table: "StudentResponses");

            migrationBuilder.DropIndex(
                name: "IX_Lab_StudentId",
                table: "Lab");

            migrationBuilder.DropIndex(
                name: "IX_InstructorResponses_ExerciseId",
                table: "InstructorResponses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Lab");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "InstructorResponses");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "StudentResponses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "InstructorResponses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "StudentLabs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLabs_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentLabs_StudentId",
                table: "StudentLabs",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentResponses_StudentLabs_StudentLabId",
                table: "StudentResponses",
                column: "StudentLabId",
                principalTable: "StudentLabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

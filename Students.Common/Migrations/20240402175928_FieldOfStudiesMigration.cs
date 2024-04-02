using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Common.Migrations
{
    /// <inheritdoc />
    public partial class FieldOfStudiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FieldOfStudyId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FieldOfStudies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationOfStudies = table.Column<double>(type: "float", nullable: false),
                    NumberOfStudents = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOfStudies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_FieldOfStudyId",
                table: "Subject",
                column: "FieldOfStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_FieldOfStudies_FieldOfStudyId",
                table: "Subject",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_FieldOfStudies_FieldOfStudyId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "FieldOfStudies");

            migrationBuilder.DropIndex(
                name: "IX_Subject_FieldOfStudyId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "FieldOfStudyId",
                table: "Subject");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Common.Migrations
{
    /// <inheritdoc />
    public partial class FixDeleteFoSMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_FieldOfStudies_FieldOfStudyId",
                table: "Subject");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_FieldOfStudies_FieldOfStudyId",
                table: "Subject",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_FieldOfStudies_FieldOfStudyId",
                table: "Subject");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_FieldOfStudies_FieldOfStudyId",
                table: "Subject",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudies",
                principalColumn: "Id");
        }
    }
}

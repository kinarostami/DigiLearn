using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Query.Migrations
{
    /// <inheritdoc />
    public partial class initSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_SubCategoryId",
                schema: "course",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Seections_SectionId",
                schema: "course",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Seections_Courses_CourseId",
                schema: "course",
                table: "Seections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seections",
                schema: "course",
                table: "Seections");

            migrationBuilder.RenameTable(
                name: "Seections",
                schema: "course",
                newName: "Sections",
                newSchema: "course");

            migrationBuilder.RenameIndex(
                name: "IX_Seections_CourseId",
                schema: "course",
                table: "Sections",
                newName: "IX_Sections_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                schema: "course",
                table: "Sections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_SubCategoryId",
                schema: "course",
                table: "Courses",
                column: "SubCategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Sections_SectionId",
                schema: "course",
                table: "Episodes",
                column: "SectionId",
                principalSchema: "course",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Courses_CourseId",
                schema: "course",
                table: "Sections",
                column: "CourseId",
                principalSchema: "course",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_SubCategoryId",
                schema: "course",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Sections_SectionId",
                schema: "course",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Courses_CourseId",
                schema: "course",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                schema: "course",
                table: "Sections");

            migrationBuilder.RenameTable(
                name: "Sections",
                schema: "course",
                newName: "Seections",
                newSchema: "course");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CourseId",
                schema: "course",
                table: "Seections",
                newName: "IX_Seections_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seections",
                schema: "course",
                table: "Seections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_SubCategoryId",
                schema: "course",
                table: "Courses",
                column: "SubCategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Seections_SectionId",
                schema: "course",
                table: "Episodes",
                column: "SectionId",
                principalSchema: "course",
                principalTable: "Seections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seections_Courses_CourseId",
                schema: "course",
                table: "Seections",
                column: "CourseId",
                principalSchema: "course",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

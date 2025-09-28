using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Query.Migrations
{
    /// <inheritdoc />
    public partial class initQueryCntexts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                schema: "course",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_SubCategoryId",
                schema: "course",
                table: "Courses");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                schema: "course",
                table: "Courses",
                column: "CategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_SubCategoryId",
                schema: "course",
                table: "Courses",
                column: "SubCategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                schema: "course",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_SubCategoryId",
                schema: "course",
                table: "Courses");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                schema: "course",
                table: "Courses",
                column: "CategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_SubCategoryId",
                schema: "course",
                table: "Courses",
                column: "SubCategoryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

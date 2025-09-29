using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class fixedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoData_IndexPage",
                schema: "course",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SeoData_Schema",
                schema: "course",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                schema: "dbo",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                schema: "dbo",
                table: "Categories",
                column: "ParentId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                schema: "dbo",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentId",
                schema: "dbo",
                table: "Categories");

            migrationBuilder.AddColumn<bool>(
                name: "SeoData_IndexPage",
                schema: "course",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SeoData_Schema",
                schema: "course",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

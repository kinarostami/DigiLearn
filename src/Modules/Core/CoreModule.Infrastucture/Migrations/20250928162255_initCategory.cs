using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class initCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cateogries",
                schema: "dbo",
                table: "Cateogries");

            migrationBuilder.RenameTable(
                name: "Cateogries",
                schema: "dbo",
                newName: "Categories",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Cateogries_Slug",
                schema: "dbo",
                table: "Categories",
                newName: "IX_Categories_Slug");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                schema: "dbo",
                table: "Categories",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                schema: "dbo",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "dbo",
                newName: "Cateogries",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Slug",
                schema: "dbo",
                table: "Cateogries",
                newName: "IX_Cateogries_Slug");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cateogries",
                schema: "dbo",
                table: "Cateogries",
                column: "Id");
        }
    }
}

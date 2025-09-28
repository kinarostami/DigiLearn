﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class inittCntexts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "course",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "course",
                table: "Courses");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    public partial class WhatIsProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Profile_ProfileId1",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_Profile_ProfileId1",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "GetProfileName",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "Profile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GetProfileName",
                table: "Profile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProfileId1",
                table: "Profile",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profile_ProfileId1",
                table: "Profile",
                column: "ProfileId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Profile_ProfileId1",
                table: "Profile",
                column: "ProfileId1",
                principalTable: "Profile",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

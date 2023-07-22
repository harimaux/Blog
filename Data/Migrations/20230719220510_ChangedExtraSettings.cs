using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    public partial class ChangedExtraSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "UserExtraStuff");

            migrationBuilder.AddColumn<string>(
                name: "CustomAvatarImage",
                table: "UserExtraStuff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockAvatarId",
                table: "UserExtraStuff",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomAvatarImage",
                table: "UserExtraStuff");

            migrationBuilder.DropColumn(
                name: "StockAvatarId",
                table: "UserExtraStuff");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "UserExtraStuff",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}

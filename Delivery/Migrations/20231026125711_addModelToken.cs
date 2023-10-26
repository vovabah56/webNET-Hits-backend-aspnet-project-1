using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Migrations
{
    /// <inheritdoc />
    public partial class addModelToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_FullName",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    InvalidToken = table.Column<string>(type: "text", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.InvalidToken);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DishId_UserId",
                table: "Ratings",
                columns: new[] { "DishId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_DishId_UserId_OrderId",
                table: "Carts",
                columns: new[] { "DishId", "UserId", "OrderId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_DishId_UserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Carts_DishId_UserId_OrderId",
                table: "Carts");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FullName",
                table: "Users",
                column: "FullName");
        }
    }
}

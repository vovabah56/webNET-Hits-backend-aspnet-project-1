using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Migrations
{
    /// <inheritdoc />
    public partial class addRatinginDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Dishes",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Dishes_DishId",
                table: "Carts",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Dishes_DishId",
                table: "Ratings",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Dishes_DishId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Dishes_DishId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Dishes");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChamealeonApp.Migrations
{
    public partial class ChangeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientMeal");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "protein",
                table: "NutrionalInformations",
                newName: "Protein");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Ingredients",
                newName: "MealId");

            migrationBuilder.AlterColumn<double>(
                name: "Calories",
                table: "NutrionalInformations",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_MealId",
                table: "Ingredients",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Meals_MealId",
                table: "Ingredients",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Meals_MealId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_MealId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "NutrionalInformations",
                newName: "protein");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "Ingredients",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "NutrionalInformations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "IngredientMeal",
                columns: table => new
                {
                    IngredientsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MealsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMeal", x => new { x.IngredientsId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_IngredientMeal_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientMeal_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMeal_MealsId",
                table: "IngredientMeal",
                column: "MealsId");
        }
    }
}

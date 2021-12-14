using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChamealeonApp.Migrations
{
    public partial class UpdateIngredientsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "Ingredients");
        }
    }
}

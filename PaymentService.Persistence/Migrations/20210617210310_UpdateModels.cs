using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Persistence.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Payments",
                newName: "PymentResultId");

            migrationBuilder.AddColumn<string>(
                name: "CardHolder",
                table: "Payments",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolder",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PymentResultId",
                table: "Payments",
                newName: "CustomerName");
        }
    }
}

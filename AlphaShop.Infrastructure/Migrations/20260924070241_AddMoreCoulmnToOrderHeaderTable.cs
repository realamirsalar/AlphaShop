using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreCoulmnToOrderHeaderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authority",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentGateway",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authority",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "PaymentGateway",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "RefId",
                table: "OrderHeaders");
        }
    }
}

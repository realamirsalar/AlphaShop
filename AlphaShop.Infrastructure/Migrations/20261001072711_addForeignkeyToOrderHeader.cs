using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addForeignkeyToOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_ApplicationUserId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_ApplicationUserId",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "OrderHeaders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderHeaders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_UserId",
                table: "OrderHeaders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_UserId",
                table: "OrderHeaders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_UserId",
                table: "OrderHeaders");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeaders_UserId",
                table: "OrderHeaders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "OrderHeaders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ApplicationUserId",
                table: "OrderHeaders",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_ApplicationUserId",
                table: "OrderHeaders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

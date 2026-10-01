using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPostRefIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefPostId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefPostId",
                table: "OrderHeaders");
        }
    }
}

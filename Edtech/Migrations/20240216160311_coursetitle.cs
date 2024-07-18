using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Edtech.Migrations
{
    /// <inheritdoc />
    public partial class coursetitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseTitle",
                table: "MerchantOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTitle",
                table: "MerchantOrders");
        }
    }
}

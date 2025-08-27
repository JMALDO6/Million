using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Million.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeInternalIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Properties_CodeInternal",
                table: "Properties",
                column: "CodeInternal",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Properties_CodeInternal",
                table: "Properties");
        }
    }
}

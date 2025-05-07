using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace amybrowbackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifyServiceDescriptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDescriptions_Services_ServiceId",
                table: "ServiceDescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceDescriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDescriptions_Services_ServiceId",
                table: "ServiceDescriptions",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDescriptions_Services_ServiceId",
                table: "ServiceDescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceDescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDescriptions_Services_ServiceId",
                table: "ServiceDescriptions",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFairManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdministratorUser_Address",
                table: "AspNetUsers",
                newName: "AddressTesterB");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "AspNetUsers",
                newName: "AddressTesterA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressTesterB",
                table: "AspNetUsers",
                newName: "AdministratorUser_Address");

            migrationBuilder.RenameColumn(
                name: "AddressTesterA",
                table: "AspNetUsers",
                newName: "Address");
        }
    }
}

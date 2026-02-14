using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentSystem.Persistence.Migrations.OTPsSec
{
    /// <inheritdoc />
    public partial class OTPSecurity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedAttempts",
                schema: "auth",
                table: "OTPs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxAttempts",
                schema: "auth",
                table: "OTPs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedAttempts",
                schema: "auth",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "MaxAttempts",
                schema: "auth",
                table: "OTPs");
        }
    }
}

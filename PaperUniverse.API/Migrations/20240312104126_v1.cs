using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperUniverse.API.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(120)", maxLength: 120, nullable: false),
                    EmailVerificationCode = table.Column<string>(type: "CHAR(6)", maxLength: 6, nullable: false),
                    EmailVerificationExpiresAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    EmailVerificationVerifiedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Password = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    PasswordResetCode = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: false),
                    Image = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbApp.Migrations.Identity
{
    /// <inheritdoc />
    public partial class ContactForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "AspNetContactFormEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetContactFormEntities", x => x.Id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetContactFormEntities");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbApp.Migrations.Identity
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetContactFormEntities",
                table: "AspNetContactFormEntities");

            migrationBuilder.RenameTable(
                name: "AspNetContactFormEntities",
                newName: "AspNetContactForms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetContactForms",
                table: "AspNetContactForms",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetContactForms",
                table: "AspNetContactForms");

            migrationBuilder.RenameTable(
                name: "AspNetContactForms",
                newName: "AspNetContactFormEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetContactFormEntities",
                table: "AspNetContactFormEntities",
                column: "Id");
        }
    }
}

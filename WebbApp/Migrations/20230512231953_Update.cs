using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbApp.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    ArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.ArticleNumber);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductArticleNumber",
                        column: x => x.ProductArticleNumber,
                        principalTable: "Products",
                        principalColumn: "ArticleNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    OnSale = table.Column<bool>(type: "bit", nullable: false),
                    StandardCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ArticleNumber);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductArticleNumber",
                        column: x => x.ProductArticleNumber,
                        principalTable: "Products",
                        principalColumn: "ArticleNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductArticleNumber",
                table: "ProductReviews",
                column: "ProductArticleNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductArticleNumber",
                table: "Stocks",
                column: "ProductArticleNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);
        }
    }
}

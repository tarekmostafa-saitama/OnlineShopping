using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberProductFavourite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    MemberId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberProductFavourite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberProductFavourite_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberProductFavourite_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberProductFavourite_MemberId",
                table: "MemberProductFavourite",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberProductFavourite_ProductId",
                table: "MemberProductFavourite",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberProductFavourite");
        }
    }
}

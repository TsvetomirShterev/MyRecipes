namespace MyRecipes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChefsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChefId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chefs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ChefId",
                table: "Recipes",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Chefs_UserId",
                table: "Chefs",
                column: "UserId",
                unique: true);


            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Chefs_ChefId",
                table: "Recipes",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Chefs_ChefId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ChefId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Recipes");
        }
    }
}

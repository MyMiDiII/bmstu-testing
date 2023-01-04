using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerING.Migrations
{
    public partial class serverAddedLinkToOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Server",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Server_OwnerID",
                table: "Server",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Server_User_OwnerID",
                table: "Server",
                column: "OwnerID",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Server_User_OwnerID",
                table: "Server");

            migrationBuilder.DropIndex(
                name: "IX_Server_OwnerID",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Server");
        }
    }
}

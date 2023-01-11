using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServerING.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LevelOfInterest = table.Column<int>(type: "integer", nullable: false),
                    OverallPlayers = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Popularity = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nickname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    HoursPlayed = table.Column<int>(type: "integer", nullable: false),
                    LastPlayed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebHosting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PricePerMonth = table.Column<int>(type: "integer", nullable: false),
                    SubMonths = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebHosting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Server",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Ip = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    GameName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    HostingID = table.Column<int>(type: "integer", nullable: false),
                    PlatformID = table.Column<int>(type: "integer", nullable: false),
                    CountryID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Server_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Server_Platform_PlatformID",
                        column: x => x.PlatformID,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Server_WebHosting_HostingID",
                        column: x => x.HostingID,
                        principalTable: "WebHosting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteServer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServerID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteServer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteServer_Server_ServerID",
                        column: x => x.ServerID,
                        principalTable: "Server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteServer_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServerID = table.Column<int>(type: "integer", nullable: false),
                    PlayerID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerPlayer_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerPlayer_Server_ServerID",
                        column: x => x.ServerID,
                        principalTable: "Server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteServer_ServerID",
                table: "FavoriteServer",
                column: "ServerID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteServer_UserID",
                table: "FavoriteServer",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Server_CountryID",
                table: "Server",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Server_HostingID",
                table: "Server",
                column: "HostingID");

            migrationBuilder.CreateIndex(
                name: "IX_Server_PlatformID",
                table: "Server",
                column: "PlatformID");

            migrationBuilder.CreateIndex(
                name: "IX_ServerPlayer_PlayerID",
                table: "ServerPlayer",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_ServerPlayer_ServerID",
                table: "ServerPlayer",
                column: "ServerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteServer");

            migrationBuilder.DropTable(
                name: "ServerPlayer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Server");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropTable(
                name: "WebHosting");
        }
    }
}

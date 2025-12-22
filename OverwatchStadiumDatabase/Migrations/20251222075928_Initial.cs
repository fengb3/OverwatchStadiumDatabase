using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OverwatchStadiumDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUri = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rarity = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroExclusives",
                columns: table => new
                {
                    HeroId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroExclusives", x => new { x.HeroId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_HeroExclusives_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroExclusives_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemBuffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    BuffId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBuffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBuffs_Buffs_BuffId",
                        column: x => x.BuffId,
                        principalTable: "Buffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemBuffs_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buffs_Name",
                table: "Buffs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeroExclusives_ItemsId",
                table: "HeroExclusives",
                column: "ItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBuffs_BuffId",
                table: "ItemBuffs",
                column: "BuffId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBuffs_ItemId",
                table: "ItemBuffs",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroExclusives");

            migrationBuilder.DropTable(
                name: "ItemBuffs");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "Buffs");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}

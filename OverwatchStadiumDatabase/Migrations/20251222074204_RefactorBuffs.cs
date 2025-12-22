using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OverwatchStadiumDatabase.Migrations
{
    /// <inheritdoc />
    public partial class RefactorBuffs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Buffs_Items_ItemId", table: "Buffs");

            migrationBuilder.DropIndex(name: "IX_Buffs_ItemId", table: "Buffs");

            migrationBuilder.DropColumn(name: "Value", table: "Buffs");

            migrationBuilder.DropColumn(name: "ItemId", table: "Buffs");

            migrationBuilder.RenameColumn(name: "BuffName", table: "Buffs", newName: "Name");

            migrationBuilder.CreateTable(
                name: "ItemBuffs",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    BuffId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBuffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBuffs_Buffs_BuffId",
                        column: x => x.BuffId,
                        principalTable: "Buffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ItemBuffs_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Buffs_Name",
                table: "Buffs",
                column: "Name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_ItemBuffs_BuffId",
                table: "ItemBuffs",
                column: "BuffId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ItemBuffs_ItemId",
                table: "ItemBuffs",
                column: "ItemId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ItemBuffs");

            migrationBuilder.DropIndex(name: "IX_Buffs_Name", table: "Buffs");

            migrationBuilder.RenameColumn(name: "Name", table: "Buffs", newName: "BuffName");

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Buffs",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m
            );

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Buffs",
                type: "INTEGER",
                nullable: true
            );

            migrationBuilder.CreateIndex(name: "IX_Buffs_ItemId", table: "Buffs", column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buffs_Items_ItemId",
                table: "Buffs",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id"
            );
        }
    }
}

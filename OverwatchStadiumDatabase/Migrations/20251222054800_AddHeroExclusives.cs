using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OverwatchStadiumDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddHeroExclusives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroExclusives_Items_ItemId",
                table: "HeroExclusives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroExclusives",
                table: "HeroExclusives");

            migrationBuilder.DropIndex(
                name: "IX_HeroExclusives_HeroId",
                table: "HeroExclusives");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HeroExclusives");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "HeroExclusives",
                newName: "ItemsId");

            migrationBuilder.RenameIndex(
                name: "IX_HeroExclusives_ItemId",
                table: "HeroExclusives",
                newName: "IX_HeroExclusives_ItemsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroExclusives",
                table: "HeroExclusives",
                columns: new[] { "HeroId", "ItemsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HeroExclusives_Items_ItemsId",
                table: "HeroExclusives",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeroExclusives_Items_ItemsId",
                table: "HeroExclusives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeroExclusives",
                table: "HeroExclusives");

            migrationBuilder.RenameColumn(
                name: "ItemsId",
                table: "HeroExclusives",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_HeroExclusives_ItemsId",
                table: "HeroExclusives",
                newName: "IX_HeroExclusives_ItemId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HeroExclusives",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeroExclusives",
                table: "HeroExclusives",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HeroExclusives_HeroId",
                table: "HeroExclusives",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeroExclusives_Items_ItemId",
                table: "HeroExclusives",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

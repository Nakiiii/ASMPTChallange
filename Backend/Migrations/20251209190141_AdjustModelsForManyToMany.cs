using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModelsForManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Orders_OrderId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_Boards_BoardId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_BoardId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Boards_OrderId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Boards");

            migrationBuilder.CreateTable(
                name: "BoardComponent",
                columns: table => new
                {
                    BoardsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ComponentsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardComponent", x => new { x.BoardsId, x.ComponentsId });
                    table.ForeignKey(
                        name: "FK_BoardComponent_Boards_BoardsId",
                        column: x => x.BoardsId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardComponent_Components_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardOrder",
                columns: table => new
                {
                    BoardsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrdersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardOrder", x => new { x.BoardsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_BoardOrder_Boards_BoardsId",
                        column: x => x.BoardsId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardComponent_ComponentsId",
                table: "BoardComponent",
                column: "ComponentsId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardOrder_OrdersId",
                table: "BoardOrder",
                column: "OrdersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardComponent");

            migrationBuilder.DropTable(
                name: "BoardOrder");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "Components",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Boards",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Components_BoardId",
                table: "Components",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_OrderId",
                table: "Boards",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Orders_OrderId",
                table: "Boards",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Boards_BoardId",
                table: "Components",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

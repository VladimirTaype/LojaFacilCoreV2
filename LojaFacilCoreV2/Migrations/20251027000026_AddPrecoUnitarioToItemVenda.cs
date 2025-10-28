using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaFacilCoreV2.Migrations
{
    /// <inheritdoc />
    public partial class AddPrecoUnitarioToItemVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Produtos",
                newName: "Estoque");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitario",
                table: "ItensVenda",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoUnitario",
                table: "ItensVenda");

            migrationBuilder.RenameColumn(
                name: "Estoque",
                table: "Produtos",
                newName: "Quantidade");
        }
    }
}

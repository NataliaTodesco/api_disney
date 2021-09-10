using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDisney.Migrations
{
    public partial class nuevaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personaje_Pelicula_Serie_idPelicula_serie",
                table: "Personaje");

            migrationBuilder.DropIndex(
                name: "IX_Personaje_idPelicula_serie",
                table: "Personaje");

            migrationBuilder.DropIndex(
                name: "IX_Pelicula_Serie_idPersonaje",
                table: "Pelicula_Serie");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_Serie_idPersonaje",
                table: "Pelicula_Serie",
                column: "idPersonaje",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pelicula_Serie_idPersonaje",
                table: "Pelicula_Serie");

            migrationBuilder.CreateIndex(
                name: "IX_Personaje_idPelicula_serie",
                table: "Personaje",
                column: "idPelicula_serie");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_Serie_idPersonaje",
                table: "Pelicula_Serie",
                column: "idPersonaje");

            migrationBuilder.AddForeignKey(
                name: "FK_Personaje_Pelicula_Serie_idPelicula_serie",
                table: "Personaje",
                column: "idPelicula_serie",
                principalTable: "Pelicula_Serie",
                principalColumn: "idPelicula_Serie",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MundoDisney.Migrations
{
    public partial class migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    idGenero = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    idPelicula_serie = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.idGenero);
                });

            migrationBuilder.CreateTable(
                name: "Personaje",
                columns: table => new
                {
                    idPersonaje = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    edad = table.Column<int>(type: "integer", nullable: false),
                    peso = table.Column<float>(type: "real", nullable: false),
                    historia = table.Column<string>(type: "text", nullable: true),
                    idPelicula_serie = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personaje", x => x.idPersonaje);
                });

            migrationBuilder.CreateTable(
                name: "Pelicula_Serie",
                columns: table => new
                {
                    idPelicula_Serie = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    titulo = table.Column<string>(type: "text", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    calificación = table.Column<int>(type: "integer", nullable: false),
                    idPersonaje = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula_Serie", x => x.idPelicula_Serie);
                    table.ForeignKey(
                        name: "FK_Pelicula_Serie_Personaje_idPersonaje",
                        column: x => x.idPersonaje,
                        principalTable: "Personaje",
                        principalColumn: "idPersonaje",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genero_idPelicula_serie",
                table: "Genero",
                column: "idPelicula_serie");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_Serie_idPersonaje",
                table: "Pelicula_Serie",
                column: "idPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_Personaje_idPelicula_serie",
                table: "Personaje",
                column: "idPelicula_serie");

            migrationBuilder.AddForeignKey(
                name: "FK_Genero_Pelicula_Serie_idPelicula_serie",
                table: "Genero",
                column: "idPelicula_serie",
                principalTable: "Pelicula_Serie",
                principalColumn: "idPelicula_Serie",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personaje_Pelicula_Serie_idPelicula_serie",
                table: "Personaje",
                column: "idPelicula_serie",
                principalTable: "Pelicula_Serie",
                principalColumn: "idPelicula_Serie",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personaje_Pelicula_Serie_idPelicula_serie",
                table: "Personaje");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Pelicula_Serie");

            migrationBuilder.DropTable(
                name: "Personaje");
        }
    }
}

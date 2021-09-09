﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MundoDisney.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Models.Genero", b =>
                {
                    b.Property<int>("idGenero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("idPelicula_serie")
                        .HasColumnType("integer");

                    b.Property<string>("nombre")
                        .HasColumnType("text");

                    b.HasKey("idGenero");

                    b.HasIndex("idPelicula_serie");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("Models.Pelicula_Serie", b =>
                {
                    b.Property<int>("idPelicula_Serie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("calificación")
                        .HasColumnType("integer");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("idPersonaje")
                        .HasColumnType("integer");

                    b.Property<string>("titulo")
                        .HasColumnType("text");

                    b.HasKey("idPelicula_Serie");

                    b.HasIndex("idPersonaje");

                    b.ToTable("Pelicula_Serie");
                });

            modelBuilder.Entity("Models.Personaje", b =>
                {
                    b.Property<int>("idPersonaje")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("edad")
                        .HasColumnType("integer");

                    b.Property<string>("historia")
                        .HasColumnType("text");

                    b.Property<int>("idPelicula_serie")
                        .HasColumnType("integer");

                    b.Property<string>("nombre")
                        .HasColumnType("text");

                    b.Property<float>("peso")
                        .HasColumnType("real");

                    b.HasKey("idPersonaje");

                    b.HasIndex("idPelicula_serie");

                    b.ToTable("Personaje");
                });

            modelBuilder.Entity("Models.Usuario", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Models.Genero", b =>
                {
                    b.HasOne("Models.Pelicula_Serie", "peliculas_series")
                        .WithMany()
                        .HasForeignKey("idPelicula_serie")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("peliculas_series");
                });

            modelBuilder.Entity("Models.Pelicula_Serie", b =>
                {
                    b.HasOne("Models.Personaje", "personajes")
                        .WithMany()
                        .HasForeignKey("idPersonaje")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("personajes");
                });

            modelBuilder.Entity("Models.Personaje", b =>
                {
                    b.HasOne("Models.Pelicula_Serie", "peliculas_series")
                        .WithMany()
                        .HasForeignKey("idPelicula_serie")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("peliculas_series");
                });
#pragma warning restore 612, 618
        }
    }
}
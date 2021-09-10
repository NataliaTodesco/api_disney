using System;
using Microsoft.AspNetCore.Http;
using Models;

namespace Comandos
{
    public class ComandoPeliculas
    {
        public int idPelicula_Serie { get; set; }
        public string titulo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int calificación { get; set; }
        public int idPersonaje{get;set;}
    }
}
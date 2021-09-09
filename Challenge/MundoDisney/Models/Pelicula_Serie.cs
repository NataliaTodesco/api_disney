using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Models    
{
    [Table("Pelicula_Serie")]
    public class Pelicula_Serie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Key]
        public int idPelicula_Serie { get; set; }
        [NotMapped]
        public IFormFile imagen { get; set; }
        public string titulo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int calificación { get; set; }
        public int idPersonaje{get;set;}
        [ForeignKey("idPersonaje")]
        public Personaje personajes { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Models
{
    [Table("Personaje")]
    public class Personaje
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Key]
        public int idPersonaje { get; set; }
        public string nombre { get; set; }
        public int edad { get; set; }
        public float peso { get; set; }
        public string historia { get; set; }
        public int idPelicula_serie{get;set;}
        public Pelicula_Serie peliculas_series { get; set; }
    }
}
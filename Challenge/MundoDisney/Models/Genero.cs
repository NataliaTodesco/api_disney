using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Models
{
    [Table("Genero")]
    public class Genero
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Key]
        public int idGenero { get; set; }
        public string nombre { get; set; }
        
        public int idPelicula_serie{get;set;}
        [ForeignKey("idPelicula_serie")]
        public Pelicula_Serie peliculas_series { get; set; }
    }
}
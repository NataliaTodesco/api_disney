using Microsoft.AspNetCore.Http;
using Models;

namespace Comandos
{
    public class ComandoPersonaje
    {
        public int idPersonaje { get; set; }
        public string nombre { get; set; }
        public int edad { get; set; }
        public float peso { get; set; }
        public string historia { get; set; }
        public int idPelicula_serie{get;set;}
    }
}
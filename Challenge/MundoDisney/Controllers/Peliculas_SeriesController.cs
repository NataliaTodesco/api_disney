using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comandos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Resultados;

namespace MundoDisney.Controllers
{
    [ApiController]
    [EnableCors("Secure")]
    public class Peliculas_SeriesController : ControllerBase
    {
        private readonly ILogger<Peliculas_SeriesController> _logger;

        public Peliculas_SeriesController(ILogger<Peliculas_SeriesController> logger)
        {
            _logger = logger;
        }

        private readonly Context db = new Context();

        [HttpGet]
        [Route("/movies")]
        public ActionResult<ResultadoApi> Get()
        {
            var resultado = new ResultadoApi();
            try
            {
                foreach (var pelicula in db.Peliculas_Series.ToList())
                {
                    resultado.Return += pelicula.imagen+" "+pelicula.titulo+" "+pelicula.fechaCreacion+"\n ";
                }
                resultado.Ok = true;

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error" + ex.Message;

                return resultado;
            }

        }

        [HttpGet]
        [Route("/movies/details")]
        public ActionResult<ResultadoApi> peliculasDetalle()
        {
            var resultado = new ResultadoApi();
            try
            {
                resultado.Return = db.Peliculas_Series.ToList();
                resultado.Ok = true;

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error" + ex.Message;

                return resultado;
            }

        }

         [HttpPost]
        [Route("/movies/create")]
        public ActionResult<ResultadoApi> AltaPersona([FromBody]ComandoPeliculas comando)
        {
            var resultado = new ResultadoApi();

            if(comando.imagen.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese imagen";
                return resultado;
            }

            if(comando.titulo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese titulo";
                return resultado;
            }

            if(comando.fechaCreacion.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese fecha de Creacion";
                return resultado;
            }

            if(comando.calificación.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese calificación";
                return resultado;
            }

            if(comando.idPersonaje.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese id del Personaje";
                return resultado;
            }

            var p = new Pelicula_Serie();
            p.idPelicula_Serie = comando.idPelicula_Serie;
            p.imagen = comando.imagen;
            p.titulo = comando.titulo;
            p.fechaCreacion = comando.fechaCreacion;
            p.calificación = comando.calificación;
            p.idPersonaje = comando.idPersonaje;

         
	        db.Entry(p).Reference(x=>x.personajes).Load();

            foreach (var c in db.Peliculas_Series)
            {
                var idPersonaje = c.idPersonaje;
                var Personaje = db.Personajes.FirstOrDefault(c => c.idPersonaje == idPersonaje);
                c.personajes = Personaje;
            }

            db.Peliculas_Series.Add(p);
            db.SaveChanges();
           
            resultado.Ok = true;
            resultado.Return = db.Personajes.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("/movies/update")]
        public ActionResult<ResultadoApi> Update([FromBody]ComandoPeliculas comando)
        {
            var resultado = new ResultadoApi();
            try
            {
                if(comando.imagen.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese imagen";
                    return resultado;
                }

                if(comando.titulo.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese titulo";
                    return resultado;
                }

                if(comando.fechaCreacion.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese fecha de Creacion";
                    return resultado;
                }

                if(comando.calificación.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese calificación";
                    return resultado;
                }

                if(comando.idPersonaje.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese id del Personaje";
                    return resultado;
                }

                var p = db.Peliculas_Series.Where(c =>c.idPelicula_Serie == comando.idPelicula_Serie).FirstOrDefault();
                if(p != null)
                {
                    p.idPelicula_Serie = comando.idPelicula_Serie;
                    p.imagen = comando.imagen;
                    p.titulo = comando.titulo;
                    p.fechaCreacion = comando.fechaCreacion;
                    p.calificación = comando.calificación;
                    p.idPersonaje = comando.idPersonaje;
            
                    db.Entry(p).Reference(x=>x.personajes).Load();

                    foreach (var c in db.Peliculas_Series)
                    {
                        var idPersonaje = c.idPersonaje;
                        var Personaje = db.Personajes.FirstOrDefault(c => c.idPersonaje == idPersonaje);
                        c.personajes = Personaje;
                    }

                    db.Peliculas_Series.Update(p);
                    db.SaveChanges();
                }

                resultado.Ok = true;
                resultado.Return = db.Personajes.ToList();;

                 return resultado;
            }
            catch (System.Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Pelicula/Serie no encontrada" + ex.Message;

                return resultado;
            }

        }

        [HttpDelete]
        [Route("/movie/delete")]
        public ActionResult<ResultadoApi> Delete([FromBody]int id)
        {
            var resultado = new ResultadoApi();
            try
            {
                var pelicula = db.Peliculas_Series.FirstOrDefault(u => u.idPelicula_Serie == id);

                if (pelicula == null)
                {
                    return NotFound($"Movie/Serie with Id = {id} not found");
                }

                db.Peliculas_Series.Remove(pelicula);
              
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Pelicula no encontrada" + ex.Message;

                return resultado;
            }
        }

        [HttpGet]
        [Route("/movie/search")]
        public ActionResult<ResultadoApi> busquedaTituloPelicula(string Titulo)
        {
            var resultado = new ResultadoApi();
            var titulo = Titulo;
            try
            {
                var peliculas = new ArrayList();
                peliculas.Add(db.Peliculas_Series.Where(u => u.titulo.Equals(titulo)));

                foreach (var per in peliculas.ToArray())
                {
                    resultado.Result.Add(per);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error" + ex.Message;

                return resultado;
            }

        }

        [HttpGet]
        [Route("/characters/search/gender")]
        public ActionResult<ResultadoApi> busquedaGeneroPelicula(String Genero)
        {
            var resultado = new ResultadoApi();
            var nombre = Genero;
            var genero = db.Generos.FirstOrDefault(u => u.nombre.Equals(nombre));

            try
            {
                var peliculas = new ArrayList();
                peliculas.Add(db.Peliculas_Series.Where(u => u.idPelicula_Serie.Equals(genero.idPelicula_serie)));

                foreach (var per in peliculas.ToArray())
                {
                    resultado.Result.Add(per);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error" + ex.Message;

                return resultado;
            }

        }

    }
}

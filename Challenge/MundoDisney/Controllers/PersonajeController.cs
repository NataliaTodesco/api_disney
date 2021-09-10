using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comandos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Resultados;

namespace MundoDisney.Controllers
{
    [ApiController]
    [EnableCors("Secure")]
    public class PersonajeController : ControllerBase
    {
        private readonly ILogger<PersonajeController> _logger;

        public PersonajeController(ILogger<PersonajeController> logger)
        {
            _logger = logger;
        }

        private readonly Context db = new Context();

        [HttpGet]
        [Route("/characters")]
        public ActionResult<ResultadoApi> Get()
        {
            var resultado = new ResultadoApi();
            try
            {
                foreach (var personaje in db.Personajes.ToList())
                {
                    resultado.Return += personaje.nombre+"\n ";
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

        [HttpPost]
        [Route("characters/create")]
        public ActionResult<ResultadoApi> AltaPersonaje([FromBody]ComandoPersonaje comando)
        {
            var resultado = new ResultadoApi();

            if(comando.nombre.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre";
                return resultado;
            }

            if(comando.edad.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese edad";
                return resultado;
            }

            if(comando.peso.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese peso";
                return resultado;
            }

            if(comando.historia.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese historia";
                return resultado;
            }

            if(comando.idPelicula_serie.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese pelicula/serie";
                return resultado;
            }

            var per = new Personaje();
            per.idPersonaje = comando.idPersonaje;
            per.nombre = comando.nombre;
            per.edad = comando.edad;
            per.peso = comando.peso;
            per.historia = comando.historia;
            per.idPelicula_serie = comando.idPelicula_serie;
            

            db.Personajes.Add(per);
            db.SaveChanges();
           
            resultado.Ok = true;
            resultado.Return = db.Personajes.ToList();

            return resultado;
        }

        [HttpPut]
        [Route("characters/update")]
        public ActionResult<ResultadoApi> Update([FromBody]ComandoPersonaje comando)
        {
            var resultado = new ResultadoApi();
            try
            {
                if(comando.nombre.Equals(""))
                {
                    resultado.Ok = false;
                    resultado.Error = "Ingrese nombre";
                    return resultado;
                }

                var per = db.Personajes.Where(c =>c.idPersonaje == comando.idPersonaje).FirstOrDefault();
                if(per != null)
                {
                    per.idPersonaje = comando.idPersonaje;
                    per.nombre = comando.nombre;
                    per.edad = comando.edad;
                    per.peso = comando.peso;
                    per.historia = comando.historia;
                    per.idPelicula_serie = comando.idPelicula_serie;
            
                    db.Personajes.Update(per);
                    db.SaveChanges();
                }

                resultado.Ok = true;
                resultado.Return = db.Personajes.ToList();;

                 return resultado;
            }
            catch (System.Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Personaje no encontrado" + ex.Message;

                return resultado;
            }

        }

        [HttpDelete]
        [Route("characters/delete")]
        public ActionResult<ResultadoApi> Delete([FromBody]int id)
        {
            var resultado = new ResultadoApi();
            try
            {
                var personaje = db.Personajes.FirstOrDefault(u => u.idPersonaje == id);

                if (personaje == null)
                {
                    return NotFound($"Character with Id = {id} not found");
                }

                db.Personajes.Remove(personaje);
              
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Personaje no encontrado" + ex.Message;

                return resultado;
            }
        }

        [HttpPost]
        [Route("character/details")]
        public ActionResult<ResultadoApi> personajeDetalle([FromBody] int idPersonaje, string Nombre)
        {
            var resultado = new ResultadoApi();
            var id = idPersonaje;
            var nombre = Nombre.Trim();
           
            try
            {
                var personaje = db.Personajes.FirstOrDefault(u => u.nombre.Equals(nombre) && u.idPersonaje.Equals(id));
                if (personaje != null)
                {
                    resultado.Ok=true;

                    resultado.Return=personaje;
                }
                else
                {
                    resultado.Ok=false;
                    resultado.Error="Personaje inexistente";
                }
                return resultado;
            }
            catch (Exception ex)
            {
                
                resultado.Ok = false;
                resultado.Error = "Personaje no encontrado" + ex.Message;

                return resultado;
            }

        }

        [HttpGet]
        [Route("/characters/details")]
        public ActionResult<ResultadoApi> personajesDetalle()
        {
            var resultado = new ResultadoApi();
            try
            {
                resultado.Return = db.Personajes.ToList();
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
        [Route("/characters/search/name")]
        public ActionResult<ResultadoApi> busquedaNombrePersonaje(String Nombre)
        {
            var resultado = new ResultadoApi();
            var nombre = Nombre.Trim();
            try
            {
                var personaje = db.Personajes.FirstOrDefault(u => u.nombre.Equals(nombre));

                resultado.Return = personaje;
                
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
        [Route("/characters/search/age")]
        public ActionResult<ResultadoApi> busquedaEdadPersonaje(int Edad)
        {
            var resultado = new ResultadoApi();
            var edad = Edad;
            try
            {
                var personaje = db.Personajes.FirstOrDefault(u => u.edad.Equals(edad));

                resultado.Return = personaje;
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
        [Route("/characters/search/weight")]
        public ActionResult<ResultadoApi> busquedaPesoPersonaje(float Peso)
        {
            var resultado = new ResultadoApi();
            var peso = Peso;
            try
            {
                var personaje = db.Personajes.FirstOrDefault(u => u.peso.Equals(peso));

                resultado.Return = personaje;
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
        [Route("/characters/search/movie")]
        public ActionResult<ResultadoApi> busquedaPersonaje(int IdPelicula)
        {
            var resultado = new ResultadoApi();
            var idPelicula = IdPelicula;
            try
            {
                var personaje = db.Personajes.FirstOrDefault(u => u.idPelicula_serie.Equals(idPelicula));

                resultado.Return = personaje;
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

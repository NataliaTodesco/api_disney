using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comandos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resultados;

namespace MundoDisney.Controllers
{
    [ApiController]
    [EnableCors("Secure")]
    public class UsuarioController : ControllerBase
    {
        // deberán desarrollarse los endpoints de registro y login, que permitan obtener el token.
        // Los endpoints encargados de la autenticación deberán ser:
        // ● /auth/register

        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
        
        private readonly Context db = new Context();

        [HttpGet]
        [Route("Usuario/ObtenerUsuarios")]
        public ActionResult<ResultadoApi> Get()
        {
            var resultado = new ResultadoApi();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Usuarios.ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Usuario no encontrado" + ex.Message;

                return resultado;
            }

        }

        [HttpPost]
        [Route("Usuario/auth/login")]
        public ActionResult<ResultadoApi> Login([FromBody] ComandoUsuario comando)
        {
            var resultado = new ResultadoApi();
            var email = comando.Email.Trim();
            var password = comando.Password;
            try
            {
                var usuario = db.Usuarios.FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
                if (usuario != null)
                {
                    resultado.Ok=true;
                    resultado.Return=usuario;
                }
                else
                {
                    resultado.Ok=false;
                    resultado.Error="Usuario o contraseña incorrectos";
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Usuario no encontrado" + ex.Message;

                return resultado;
            }

        }

    }
}

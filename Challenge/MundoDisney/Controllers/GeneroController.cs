using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resultados;

namespace MundoDisney.Controllers
{
    [ApiController]
    [EnableCors("Secure")]
    public class GeneroController : ControllerBase
    {
        private readonly ILogger<GeneroController> _logger;
        private readonly Context db = new Context();

        public GeneroController(ILogger<GeneroController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Genero/ObtenerGeneros")]
        public ActionResult<ResultadoApi> Get()
        {
            var resultado = new ResultadoApi();
            try
            {
                resultado.Ok = true;
                resultado.Return = db.Generos.ToList();
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

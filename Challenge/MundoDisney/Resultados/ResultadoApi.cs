using System.Collections;
namespace Resultados
{
    public class ResultadoApi
    {
        public bool Ok { get; set; }
        public string Error{ get; set; }
        public object Return { get; set; }
        public ArrayList Result { get; set; }
    }
}
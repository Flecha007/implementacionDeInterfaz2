using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion1.Models.Request
{
    class VentasRequest
    {
        public string nombre_cliente { get; set; }
        public int doc_cliente { get; set; }
        public string nombre_paquete { get; set; }
        public int cantidad_dias { get; set; }
        public int total { get; set; }
        public string nombre_usuario { get; set; }
        public string fecha { get; set; }
    }
}

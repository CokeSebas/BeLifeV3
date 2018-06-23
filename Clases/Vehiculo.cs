using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Vehiculo:Contrato
    {
        public string Patente { get; set; }
        public int marca { get; set; }
        public int modelo { get; set; }
        public int anio { get; set; }

    }
}

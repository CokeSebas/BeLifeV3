using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Vivienda:Contrato{
        public string CodigoPostal {get;set;}
        public int anio { get; set; }
        public string direccion { get; set; }
        public int valorInmu { get; set; }
        public int valorConte { get; set; }
        public int region { get; set; }
        public int comuna { get; set; }
    }
}

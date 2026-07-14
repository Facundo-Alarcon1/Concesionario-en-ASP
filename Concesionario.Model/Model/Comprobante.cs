using System;
using System.Collections.Generic;
using System.Text;

namespace ConcesionarioWEBFORM1111.Model
{
    public class Comprobante
    { // atributos de comprobante que estan en la base de datos
        public int ID_comprobante { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaHora { get; set; }
        public int ID_auto { get; set; }
        public int ID_empleado { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public decimal Precio { get; set; } 
       
        }
    }



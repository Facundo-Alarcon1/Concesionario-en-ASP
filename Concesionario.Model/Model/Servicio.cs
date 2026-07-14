using System;
using System.Collections.Generic;
using System.Text;

namespace ConcesionarioWEBFORM1111.Model
{
    public class Servicio
    { // atributos de Servicios que estan en la base de datos
        public int ID_servicio { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int ID_empleado { get; set; }
        public string NombreEmpleado { get; set; }
    }
}


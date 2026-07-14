using System;
using System.Collections.Generic;
using System.Text;

namespace ConcesionarioWEBFORM1111.Model
{
    public class Empleados
    {
        public int ID_empleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Puesto { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}



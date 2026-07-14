using System;
using System.Collections.Generic;
using System.Text;

namespace ConcesionarioWEBFORM1111.Model
{
    public class Auto
    { // atributos de autos que estan en la base de datos
        public int ID_auto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Patente { get; set; }
        public int Anio { get; set; }
        public string Estado { get; set; }

        public decimal Precio { get; set; }
        public int ID_empleado { get; set; }
    }
}


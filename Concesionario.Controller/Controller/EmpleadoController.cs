using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;
using ConcesionarioWEBFORM1111.DataBase.DAO;

namespace ConcesionarioWEBFORM1111.Controller
{
    public class EmpleadoController
    {
        private readonly IEmpleadoDAO empleadoDao;

        public EmpleadoController(IEmpleadoDAO empleadoDao)
        {
            this.empleadoDao = empleadoDao;
        }

        public async Task<List<Empleados>> ObtenerEmpleadosAsync()
        {
            return await empleadoDao.ObtenerEmpleadosAsync();
        }

        public async Task<bool> AgregarEmpleadoAsync(Empleados empleado)
        {
            return await empleadoDao.AgregarEmpleadoAsync(empleado);
        }

        public async Task<bool> ModificarEmpleadoAsync(Empleados empleado)
        {
            return await empleadoDao.ModificarEmpleadoAsync(empleado);
        }

        public async Task<bool> EliminarEmpleadoAsync(int idEmpleado)
        {
            return await empleadoDao.EliminarEmpleadoAsync(idEmpleado);
        }
    }
}

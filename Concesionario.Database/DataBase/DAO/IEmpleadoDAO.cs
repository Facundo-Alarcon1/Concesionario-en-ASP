using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public interface IEmpleadoDAO
    {
        Task<List<Empleados>> ObtenerEmpleadosAsync();
        Task<bool> AgregarEmpleadoAsync(Empleados empleado);
        Task<bool> ModificarEmpleadoAsync(Empleados empleado);
        Task<bool> EliminarEmpleadoAsync(int idEmpleado);
    }
}

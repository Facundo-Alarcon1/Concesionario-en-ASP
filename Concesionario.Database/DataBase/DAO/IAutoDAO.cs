using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public interface IAutoDAO
    {
        Task<bool> EmpleadoExisteAsync(int idEmpleado);
        Task<bool> AgregarAutoYComprobanteAsync(Auto auto);
        Task<bool> ActualizarCampoAutoAsync(int idAuto, string campo, object nuevoValor);
        Task<bool> ModificarAutoAsync(Auto auto);
        Task<bool> EliminarAutoAsync(int idAuto);
        Task<List<Auto>> BuscarAutoAsync(string criterio);
        Task<bool> VenderAutoAsync(int idAuto, int idEmpleado, string observaciones);
        Task<List<Auto>> ObtenerTodosLosAutosAsync();
        Task<List<Auto>> BuscarAutosPorEstadoAsync(string estado);
    }
}


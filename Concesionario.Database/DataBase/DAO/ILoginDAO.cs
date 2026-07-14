using System;
using System.Threading.Tasks;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public interface ILoginDAO
    {
        Task<(string Puesto, int IdEmpleado, string NombreUsuario, string ContraseniaHash)> ObtenerUsuarioPorNombreAsync(string nombreUsuario);
    }
}


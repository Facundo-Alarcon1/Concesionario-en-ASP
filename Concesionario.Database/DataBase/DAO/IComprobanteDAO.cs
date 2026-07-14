using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public interface IComprobanteDAO
    {
        Task<List<Comprobante>> ObtenerTodosLosComprobantesAsync();
    }
}


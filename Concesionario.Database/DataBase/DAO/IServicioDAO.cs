using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public interface IServicioDAO
    {
        Task<bool> AgregarServicioAsync(Servicio servicio);
        Task<bool> MarcarComoRealizadoAsync(int idServicio);
        Task<bool> EliminarServicioAsync(int idServicio);
        Task<List<Servicio>> ObtenerTodosLosServiciosAsync();
    }
}


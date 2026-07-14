using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111.Controller
{
    public class ServicioController
    {
        private readonly IServicioDAO servicioDao;

        public ServicioController(IServicioDAO servicioDao)
        {
            this.servicioDao = servicioDao;
        }

        public async Task<bool> AgregarServicioAsync(Servicio servicio)
        {
            return await servicioDao.AgregarServicioAsync(servicio);
        }

        public async Task<bool> MarcarComoRealizadoAsync(int idServicio)
        {
            return await servicioDao.MarcarComoRealizadoAsync(idServicio);
        }

        public async Task<bool> EliminarServicioAsync(int idServicio)
        {
            return await servicioDao.EliminarServicioAsync(idServicio);
        }

        public async Task<List<Servicio>> ObtenerTodosLosServiciosAsync()
        {
            return await servicioDao.ObtenerTodosLosServiciosAsync();
        }
    }
}


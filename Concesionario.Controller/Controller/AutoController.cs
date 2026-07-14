using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model.Utilities;

namespace ConcesionarioWEBFORM1111.Controller
{
    public class AutoController
    {
        private readonly IAutoDAO autoDao;

        public AutoController(IAutoDAO autoDao)
        {
            this.autoDao = autoDao;
        }

        public async Task<bool> AgregarAutoAsync(Auto auto, Empleados empleado)
        {
            if (empleado.Puesto != "Gerente")
            {
                Logger.LogError("Intento de agregar auto por un no-Gerente.");
                return false;
            }

            if (!await autoDao.EmpleadoExisteAsync(auto.ID_empleado))
            {
                Logger.LogError($"El ID_empleado {auto.ID_empleado} no existe en la tabla Empleados.");
                return false;
            }

            if (await autoDao.AgregarAutoYComprobanteAsync(auto))
            {
                Logger.LogInfo("El auto se agregÃ³ correctamente y se generÃ³ el comprobante.");
                return true;
            }
            else
            {
                Logger.LogError("No se pudo agregar el auto.");
                return false;
            }
        }

        public async Task<bool> ModificarAutoAsync(Auto auto)
        {
            return await autoDao.ModificarAutoAsync(auto);
        }

        public async Task<bool> ActualizarPrecioAutoAsync(int idAuto, decimal nuevoPrecio)
        {
            return await autoDao.ActualizarCampoAutoAsync(idAuto, "Precio", nuevoPrecio);
        }

        public async Task<bool> ActualizarMarcaAutoAsync(int idAuto, string nuevaMarca)
        {
            return await autoDao.ActualizarCampoAutoAsync(idAuto, "Marca", nuevaMarca);
        }

        public async Task<bool> ActualizarModeloAutoAsync(int idAuto, string nuevoModelo)
        {
            return await autoDao.ActualizarCampoAutoAsync(idAuto, "Modelo", nuevoModelo);
        }

        public async Task<bool> ActualizarColorAutoAsync(int idAuto, string nuevoColor)
        {
            return await autoDao.ActualizarCampoAutoAsync(idAuto, "Color", nuevoColor);
        }

        public async Task<bool> ActualizarPatenteAutoAsync(int idAuto, string nuevaPatente)
        {
            return await autoDao.ActualizarCampoAutoAsync(idAuto, "Patente", nuevaPatente);
        }

        public async Task<bool> ActualizarAnioAutoAsync(int idAuto, int nuevoAnio)
        {
            return await autoDao.ActualizarCampoAutoAsync(idAuto, "Anio", nuevoAnio);
        }

        public async Task<bool> EliminarAutoAsync(int idAuto)
        {
            if (await autoDao.EliminarAutoAsync(idAuto))
            {
                Logger.LogInfo("El auto se eliminÃ³ correctamente.");
                return true;
            }
            return false;
        }

        public async Task<List<Auto>> BuscarAutoAsync(string criterio)
        {
            return await autoDao.BuscarAutoAsync(criterio);
        }

        public async Task<bool> VenderAutoAsync(int idAuto, int idEmpleado, string observaciones)
        {
            return await autoDao.VenderAutoAsync(idAuto, idEmpleado, observaciones);
        }

        public async Task<List<Auto>> ObtenerTodosLosAutosAsync()
        {
            return await autoDao.ObtenerTodosLosAutosAsync();
        }

        public async Task<List<Auto>> BuscarAutosPorEstadoAsync(string estado)
        {
            return await autoDao.BuscarAutosPorEstadoAsync(estado);
        }
    }
}


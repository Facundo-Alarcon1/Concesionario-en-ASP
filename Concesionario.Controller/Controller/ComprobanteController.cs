using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111.Controller
{
    public class ComprobanteController
    {
        private readonly IComprobanteDAO comprobanteDao;

        public ComprobanteController(IComprobanteDAO comprobanteDao)
        {
            this.comprobanteDao = comprobanteDao;
        }

        public async Task<List<Comprobante>> ObtenerTodosLosComprobantesAsync()
        {
            return await comprobanteDao.ObtenerTodosLosComprobantesAsync();
        }
    }
}


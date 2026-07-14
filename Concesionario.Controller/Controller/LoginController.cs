using System;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111.Controller
{
    public class LoginController
    {
        private readonly ILoginDAO loginDao;

        public LoginController(ILoginDAO loginDao)
        {
            this.loginDao = loginDao;
        }

        public async Task<(string Puesto, int IdEmpleado, string NombreUsuario, string Contrasenia)> ValidarCredencialesAsync(string nombreUsuario, string contraseniaIngresada)
        {
            var usuario = await loginDao.ObtenerUsuarioPorNombreAsync(nombreUsuario);

            if (usuario.NombreUsuario != null)
            {
                // Verifica el hash, o permite texto plano si el usuario lo insertó manualmente en la base de datos para pruebas
                if (SecurityHelper.VerifyPassword(contraseniaIngresada, usuario.ContraseniaHash) || contraseniaIngresada == usuario.ContraseniaHash)
                {
                    return (usuario.Puesto, usuario.IdEmpleado, usuario.NombreUsuario, usuario.ContraseniaHash);
                }
            }

            return (null, 0, null, null);
        }

        public async Task<bool> PuedeVenderAutosAsync(string nombreUsuario, string contrasenia)
        {
            var (puesto, _, _, _) = await ValidarCredencialesAsync(nombreUsuario, contrasenia);
            return puesto == "Empleado" || puesto == "Gerente";
        }

        public async Task<bool> PuedeAgregarAutosAsync(string nombreUsuario, string contrasenia)
        {
            var (puesto, _, _, _) = await ValidarCredencialesAsync(nombreUsuario, contrasenia);
            return puesto == "Gerente";
        }
    }
}


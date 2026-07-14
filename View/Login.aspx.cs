using System;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.DataBase;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111
{
    public partial class Login : System.Web.UI.Page
    {
        private LoginController loginController;

        protected void Page_Init(object sender, EventArgs e)
        {
            var db = new DataBaseConnection();
            var loginDao = new LoginDAO(db);
            loginController = new LoginController(loginDao);
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validar credenciales con el controlador de forma asíncrona
            var resultado = await loginController.ValidarCredencialesAsync(usuario, password);

            if (resultado.NombreUsuario != null)
            {
                // Crear objeto Empleados con los datos
                var empleado = new ConcesionarioWEBFORM1111.Model.Empleados
                {
                    ID_empleado = resultado.IdEmpleado,
                    Nombre = resultado.NombreUsuario,
                    Apellido = "",
                    Puesto = resultado.Puesto
                };

                // Guardar el objeto completo en sesión
                Session["usuario"] = empleado;

                // Redirigir a la página principal
                Response.Redirect("Home.aspx", false);
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}


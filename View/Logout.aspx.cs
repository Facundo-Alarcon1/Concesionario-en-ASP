using System;

namespace ConcesionarioWEBFORM1111
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cerrar sesión
            Session.Abandon();

            // Redirigir a la página de login
            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}


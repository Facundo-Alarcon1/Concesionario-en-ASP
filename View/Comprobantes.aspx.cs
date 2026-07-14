using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.DataBase;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111
{
    public partial class Comprobantes : System.Web.UI.Page
    {
        private ComprobanteController comprobanteController;

        protected void Page_Init(object sender, EventArgs e)
        {
            var db = new DataBaseConnection();
            comprobanteController = new ComprobanteController(new ComprobanteDAO(db));
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                await CargarComprobantesAsync();
            }
        }

        private async Task CargarComprobantesAsync(string tipoFiltro = "")
        {
            List<Comprobante> lista = await comprobanteController.ObtenerTodosLosComprobantesAsync();

            if (!string.IsNullOrEmpty(tipoFiltro))
            {
                lista = lista.FindAll(c => c.Tipo.Equals(tipoFiltro, StringComparison.OrdinalIgnoreCase));
            }

            gvComprobantes.DataSource = lista;
            gvComprobantes.DataBind();
        }

        protected async void btnFiltrar_Click(object sender, EventArgs e)
        {
            string tipoFiltro = ddlTipoFiltro.SelectedValue;
            await CargarComprobantesAsync(tipoFiltro);
        }
    }
}


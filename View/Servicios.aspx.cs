using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.DataBase;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111
{
    public partial class Servicios : Page
    {
        private ServicioController servicioController;
        private EmpleadoController empleadoController;

        protected void Page_Init(object sender, EventArgs e)
        {
            var db = new DataBaseConnection();
            servicioController = new ServicioController(new ServicioDAO(db));
            empleadoController = new EmpleadoController(new EmpleadoDAO(db));
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
                await CargarEmpleadosAsync();
                await CargarServiciosAsync();
                lblMensaje.Text = ""; 
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd"); // Fecha por defecto
            }
        }

        private async Task CargarEmpleadosAsync()
        {
            var empleados = await empleadoController.ObtenerEmpleadosAsync();
            ddlEmpleados.DataSource = empleados;
            ddlEmpleados.DataTextField = "Nombre"; // 'Nombre' exists in Empleados model, I'll use it
            ddlEmpleados.DataValueField = "ID_empleado";
            ddlEmpleados.DataBind();
            ddlEmpleados.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un empleado...", ""));
        }

        private async Task CargarServiciosAsync()
        {
            var lista = await servicioController.ObtenerTodosLosServiciosAsync(); 
            lista.Sort((a, b) => b.Fecha.CompareTo(a.Fecha));

            gvServicios.DataSource = lista;
            gvServicios.DataBind();
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = esExito ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MostrarMensaje("La descripción es obligatoria.", false);
                return;
            }

            if (!DateTime.TryParse(txtFecha.Text, out DateTime fecha))
            {
                MostrarMensaje("Fecha inválida.", false);
                return;
            }

            if (string.IsNullOrEmpty(ddlEmpleados.SelectedValue))
            {
                MostrarMensaje("Debe seleccionar un mecánico.", false);
                return;
            }

            var servicio = new ConcesionarioWEBFORM1111.Model.Servicio
            {
                Descripcion = txtDescripcion.Text.Trim(),
                Fecha = fecha,
                ID_empleado = int.Parse(ddlEmpleados.SelectedValue)
            };

            bool exito = await servicioController.AgregarServicioAsync(servicio);
            
            if (exito)
            {
                MostrarMensaje("Servicio agendado correctamente.", true);
                await CargarServiciosAsync();
                txtDescripcion.Text = string.Empty;
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                ddlEmpleados.SelectedIndex = 0;
            }
            else
            {
                MostrarMensaje("Error al agendar el servicio.", false);
            }
        }

        protected async void gvServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idServicio = Convert.ToInt32(gvServicios.DataKeys[index].Value);

            if (e.CommandName == "Realizado")
            {
                await servicioController.MarcarComoRealizadoAsync(idServicio);
            }
            else if (e.CommandName == "Eliminar")
            {
                await servicioController.EliminarServicioAsync(idServicio);
            }

            await CargarServiciosAsync();
        }
    }
}

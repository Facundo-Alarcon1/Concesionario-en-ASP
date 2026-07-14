using System;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.DataBase;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111
{
    public partial class Autos : System.Web.UI.Page
    {
        private AutoController autoController;

        protected void Page_Init(object sender, EventArgs e)
        {
            var db = new DataBaseConnection();
            var autoDao = new AutoDAO(db);
            autoController = new AutoController(autoDao);
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
                VerificarPermisos();
                await CargarAutosAsync();
            }

            var empleado = (ConcesionarioWEBFORM1111.Model.Empleados)Session["usuario"];
            lblUsuario.Text = $"Hola, {empleado.Nombre} ({empleado.Puesto})";
        }

        private void VerificarPermisos()
        {
            var empleado = (ConcesionarioWEBFORM1111.Model.Empleados)Session["usuario"];

            if (empleado.Puesto != "Gerente")
            {
                pnlAgregarAuto.Visible = false;
                gvAutos.Columns[8].Visible = false; // Editar
                gvAutos.Columns[9].Visible = false; // Eliminar
            }
        }

        private async Task CargarAutosAsync()
        {
            var autos = await autoController.BuscarAutosPorEstadoAsync("disponible");
            gvAutos.DataSource = autos;
            gvAutos.DataBind();
        }

        protected async void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                var autos = await autoController.BuscarAutoAsync(criterio);
                gvAutos.DataSource = autos;
                gvAutos.DataBind();
            }
            else
            {
                await CargarAutosAsync();
            }
        }

        protected async void btnVerTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            await CargarAutosAsync();
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMarca.Text) || string.IsNullOrWhiteSpace(txtModelo.Text) || 
                string.IsNullOrWhiteSpace(txtColor.Text) || string.IsNullOrWhiteSpace(txtPatente.Text))
            {
                MostrarMensaje("Todos los campos de texto son obligatorios.", false);
                return;
            }

            if (!int.TryParse(txtAnio.Text, out int anio) || anio < 1900 || anio > DateTime.Now.Year + 1)
            {
                MostrarMensaje("Año inválido. Debe ser un número realista.", false);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MostrarMensaje("Precio inválido. Debe ser un número mayor a 0.", false);
                return;
            }

            var auto = new ConcesionarioWEBFORM1111.Model.Auto
            {
                Marca = txtMarca.Text.Trim(),
                Modelo = txtModelo.Text.Trim(),
                Color = txtColor.Text.Trim(),
                Patente = txtPatente.Text.Trim(),
                Anio = anio,
                Precio = precio,
                Estado = "Disponible"
            };

            bool exito;
            if (string.IsNullOrEmpty(hfAutoId.Value))
            {
                var empleado = (ConcesionarioWEBFORM1111.Model.Empleados)Session["usuario"];
                exito = await autoController.AgregarAutoAsync(auto, empleado);
                if (exito) MostrarMensaje("Vehículo agregado correctamente.", true);
            }
            else
            {
                auto.ID_auto = int.Parse(hfAutoId.Value);
                exito = await autoController.ModificarAutoAsync(auto);
                if (exito) MostrarMensaje("Vehículo actualizado correctamente.", true);
                btnCancelar_Click(null, null);
            }

            if (!exito) MostrarMensaje("Error al guardar el vehículo.", false);

            await CargarAutosAsync();
            LimpiarFormulario();
        }

        protected async void gvAutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Vender")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idAuto = Convert.ToInt32(gvAutos.DataKeys[index].Value);
                var empleado = (ConcesionarioWEBFORM1111.Model.Empleados)Session["usuario"];

                bool exito = await autoController.VenderAutoAsync(idAuto, empleado.ID_empleado, "Venta desde Web");

                if (exito)
                {
                    lblGridMensaje.Text = "Auto vendido exitosamente.";
                    lblGridMensaje.ForeColor = System.Drawing.Color.Green;
                    await CargarAutosAsync();
                }
                else
                {
                    lblGridMensaje.Text = "Error al vender auto.";
                    lblGridMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gvAutos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAutos.EditIndex = e.NewEditIndex;
            // Para mantener el principio async/await y ASP.NET forms cycle, 
            // llamamos a CargarAutos de forma sincrÃ³nica o registramos la tarea
            RegisterAsyncTask(new System.Web.UI.PageAsyncTask(CargarAutosAsync));
        }

        protected void gvAutos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAutos.EditIndex = -1;
            RegisterAsyncTask(new System.Web.UI.PageAsyncTask(CargarAutosAsync));
        }

        protected async void gvAutos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idAuto = Convert.ToInt32(gvAutos.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvAutos.Rows[e.RowIndex];

            string marca = ((TextBox)row.Cells[1].Controls[0]).Text;
            string modelo = ((TextBox)row.Cells[2].Controls[0]).Text;
            string color = ((TextBox)row.Cells[3].Controls[0]).Text;
            string patente = ((TextBox)row.Cells[4].Controls[0]).Text;
            int anio = int.Parse(((TextBox)row.Cells[5].Controls[0]).Text);
            decimal precio = decimal.Parse(((TextBox)row.Cells[6].Controls[0]).Text);

            await autoController.ActualizarMarcaAutoAsync(idAuto, marca);
            await autoController.ActualizarModeloAutoAsync(idAuto, modelo);
            await autoController.ActualizarColorAutoAsync(idAuto, color);
            await autoController.ActualizarPatenteAutoAsync(idAuto, patente);
            await autoController.ActualizarAnioAutoAsync(idAuto, anio);
            await autoController.ActualizarPrecioAutoAsync(idAuto, precio);

            gvAutos.EditIndex = -1;
            await CargarAutosAsync();
        }

        protected async void gvAutos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idAuto = Convert.ToInt32(gvAutos.DataKeys[e.RowIndex].Value);
            bool exito = await autoController.EliminarAutoAsync(idAuto);

            if (exito)
            {
                lblGridMensaje.Text = "Auto eliminado.";
                lblGridMensaje.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblGridMensaje.Text = "Error al eliminar auto.";
                lblGridMensaje.ForeColor = System.Drawing.Color.Red;
            }

            await CargarAutosAsync();
        }

        private void LimpiarFormulario()
        {
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtColor.Text = "";
            txtPatente.Text = "";
            txtAnio.Text = "";
            txtPrecio.Text = "";
            hfAutoId.Value = "";
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblGridMensaje.Text = mensaje;
            lblGridMensaje.ForeColor = esExito ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblGridMensaje.Text = "";
        }
    }
}


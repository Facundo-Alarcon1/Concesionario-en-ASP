using System;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.DataBase;
using ConcesionarioWEBFORM1111.DataBase.DAO;
using ConcesionarioWEBFORM1111.Model;
using System.Collections.Generic;
using System.Drawing;

namespace ConcesionarioWEBFORM1111
{
    public partial class Empleados : System.Web.UI.Page
    {
        private EmpleadoController empleadoController;

        protected void Page_Init(object sender, EventArgs e)
        {
            var db = new DataBaseConnection();
            var empleadoDao = new EmpleadoDAO(db);
            empleadoController = new EmpleadoController(empleadoDao);
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null || ((ConcesionarioWEBFORM1111.Model.Empleados)Session["usuario"]).Puesto.ToLower() != "gerente")
            {
                Response.Redirect("Home.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                await CargarEmpleadosAsync();
            }
        }

        private async Task CargarEmpleadosAsync()
        {
            try
            {
                var empleados = await empleadoController.ObtenerEmpleadosAsync();
                gvEmpleados.DataSource = empleados;
                gvEmpleados.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar empleados: " + ex.Message, false);
            }
        }

        protected async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MostrarMensaje("Nombre y Apellido son obligatorios.", false);
                return;
            }

            var empleado = new ConcesionarioWEBFORM1111.Model.Empleados
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Puesto = ddlPuesto.SelectedValue
            };

            bool exito;
            if (string.IsNullOrEmpty(hfEmpleadoId.Value))
            {
                exito = await empleadoController.AgregarEmpleadoAsync(empleado);
                if (exito) MostrarMensaje("Empleado agregado correctamente.", true);
            }
            else
            {
                empleado.ID_empleado = int.Parse(hfEmpleadoId.Value);
                exito = await empleadoController.ModificarEmpleadoAsync(empleado);
                if (exito) MostrarMensaje("Empleado actualizado correctamente.", true);
                btnCancelar_Click(null, null);
            }

            if (!exito) MostrarMensaje("Error al guardar el empleado.", false);

            await CargarEmpleadosAsync();
            LimpiarFormulario();
        }

        protected async void gvEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectEdit" || e.CommandName == "DeleteEmp")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idEmpleado = Convert.ToInt32(gvEmpleados.DataKeys[index].Value);

                if (e.CommandName == "SelectEdit")
                {
                    hfEmpleadoId.Value = idEmpleado.ToString();
                    txtNombre.Text = gvEmpleados.Rows[index].Cells[1].Text;
                    txtApellido.Text = gvEmpleados.Rows[index].Cells[2].Text;
                    ddlPuesto.SelectedValue = gvEmpleados.Rows[index].Cells[3].Text;

                    btnGuardar.Text = "Actualizar";
                    btnCancelar.Visible = true;
                    lblMensaje.Text = "";
                }
                else if (e.CommandName == "DeleteEmp")
                {
                    var empleadoActual = (ConcesionarioWEBFORM1111.Model.Empleados)Session["usuario"];
                    if (empleadoActual.ID_empleado == idEmpleado)
                    {
                        MostrarMensaje("No puedes eliminar tu propio usuario.", false);
                        return;
                    }

                    bool exito = await empleadoController.EliminarEmpleadoAsync(idEmpleado);
                    if (exito)
                    {
                        MostrarMensaje("Empleado eliminado correctamente.", true);
                        await CargarEmpleadosAsync();
                    }
                    else
                    {
                        MostrarMensaje("Error al eliminar el empleado.", false);
                    }
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            btnGuardar.Text = "Guardar";
            btnCancelar.Visible = false;
            hfEmpleadoId.Value = string.Empty;
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            ddlPuesto.SelectedIndex = 0;
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.ForeColor = esExito ? Color.Green : Color.Red;
            lblMensaje.CssClass = esExito ? "text-xs font-medium block mb-2 text-emerald-600" : "text-xs font-medium block mb-2 text-rose-600";
        }
    }
}

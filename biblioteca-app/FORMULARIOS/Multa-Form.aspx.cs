using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Multa_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarPrestamos();
                CargarMultas();
            }
        }

        private void CargarUsuarios()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_usuarios");
            ddlUsuario.DataSource = dt;
            ddlUsuario.DataTextField = "nombre";
            ddlUsuario.DataValueField = "id_usuario";
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("-- Seleccione un usuario --", "0"));
        }

        private void CargarPrestamos()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_prestamos");
            ddlPrestamo.DataSource = dt;
            ddlPrestamo.DataTextField = "id_prestamo";
            ddlPrestamo.DataValueField = "id_prestamo";
            ddlPrestamo.DataBind();
            ddlPrestamo.Items.Insert(0, new ListItem("-- Seleccione un préstamo --", "0"));
        }

        private void CargarMultas()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_multas");
            gvMultas.DataSource = dt;
            gvMultas.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                decimal monto = 0;
                decimal.TryParse(txtMonto.Text.Trim(), out monto);

                Multa nuevaMulta = new Multa(
                    txtTipoMulta.Text.Trim(),
                    monto,
                    txtDescripcion.Text.Trim(),
                    ddlEstado.SelectedValue,
                    int.Parse(ddlUsuario.SelectedValue),
                    int.Parse(ddlPrestamo.SelectedValue)
                );

                nuevaMulta.Save_Multa();
                LimpiarFormulario();
                CargarMultas();
            }
        }

        protected void gvMultas_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            txtTipoMulta.Text = "";
            txtMonto.Text = "";
            txtDescripcion.Text = "";
            ddlUsuario.SelectedIndex = 0;
            ddlPrestamo.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;
        }
    }
}
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Prestamo_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarEjemplares();
                CargarPrestamos();
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

        private void CargarEjemplares()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_ejemplares");
            ddlEjemplar.DataSource = dt;
            ddlEjemplar.DataTextField = "titulo_libro";
            ddlEjemplar.DataValueField = "id_ejemplar";
            ddlEjemplar.DataBind();
            ddlEjemplar.Items.Insert(0, new ListItem("-- Seleccione un ejemplar --", "0"));
        }

        private void CargarPrestamos()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_prestamos");
            gvPrestamos.DataSource = dt;
            gvPrestamos.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DateTime fechaLimite = DateTime.MinValue;
                if (!string.IsNullOrEmpty(txtFechaLimite.Text))
                    DateTime.TryParse(txtFechaLimite.Text, out fechaLimite);

                Prestamo nuevoPrestamo = new Prestamo(
                    int.Parse(ddlUsuario.SelectedValue),
                    fechaLimite,
                    txtObservaciones.Text.Trim(),
                    int.Parse(ddlEjemplar.SelectedValue)
                );

                nuevoPrestamo.Save_Prestamo();
                LimpiarFormulario();
                CargarPrestamos();
            }
        }

        protected void gvPrestamos_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            ddlUsuario.SelectedIndex = 0;
            ddlEjemplar.SelectedIndex = 0;
            txtFechaLimite.Text = "";
            txtObservaciones.Text = "";
        }
    }
}
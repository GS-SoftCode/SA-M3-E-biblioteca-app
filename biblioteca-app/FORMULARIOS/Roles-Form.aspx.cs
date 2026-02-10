using System;
using System.Data;
using System.Web.UI;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Roles_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarRoles();
        }

        private void CargarRoles()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_roles");
            gvRoles.DataSource = dt;
            gvRoles.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Rol nuevoRol = new Rol(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                nuevoRol.Save_Rol();
                LimpiarFormulario();
                CargarRoles();
            }
        }

        protected void gvRoles_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }
    }
}
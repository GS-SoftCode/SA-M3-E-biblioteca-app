using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Usuario_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
                CargarUsuarios();
            }
        }

        private void CargarRoles()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_roles");
            ddlRol.DataSource = dt;
            ddlRol.DataTextField = "nombre";
            ddlRol.DataValueField = "id_rol";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem("-- Seleccione un rol --", "0"));
        }

        private void CargarUsuarios()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_usuarios");
            gvUsuarios.DataSource = dt;
            gvUsuarios.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Usuario nuevoUsuario = new Usuario(
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    int.Parse(ddlRol.SelectedValue)
                );

                nuevoUsuario.Save_Usuario();
                LimpiarFormulario();
                CargarUsuarios();
            }
        }

        protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            ddlRol.SelectedIndex = 0;
            chkActivo.Checked = true;
        }
    }
}
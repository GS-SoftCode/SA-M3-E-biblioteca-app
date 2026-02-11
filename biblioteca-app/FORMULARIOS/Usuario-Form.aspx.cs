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

        private bool EmailExiste(string email)
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_usuarios");
            foreach (DataRow row in dt.Rows)
            {
                if (row["email"].ToString().ToLower() == email.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string email = txtEmail.Text.Trim();

                // Validar si el email ya existe
                if (EmailExiste(email))
                {
                    // Mostrar mensaje de error
                    ScriptManager.RegisterStartupScript(this, GetType(), "EmailDuplicado",
                        "alert('Error: El email " + email + " ya está registrado. Por favor, use otro correo electrónico.');", true);
                    return;
                }

                try
                {
                    Usuario nuevoUsuario = new Usuario(
                        txtNombre.Text.Trim(),
                        txtApellido.Text.Trim(),
                        email,
                        txtTelefono.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        int.Parse(ddlRol.SelectedValue)
                    );

                    nuevoUsuario.Save_Usuario();
                    LimpiarFormulario();
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Error",
                        "alert('Error al guardar el usuario: " + ex.Message + "');", true);
                }
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
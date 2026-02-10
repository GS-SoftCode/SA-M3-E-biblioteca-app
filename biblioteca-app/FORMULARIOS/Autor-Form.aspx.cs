using System;
using System.Data;
using System.Web.UI;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Autor_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarAutores();
        }

        private void CargarAutores()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_autores");
            gvAutores.DataSource = dt;
            gvAutores.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DateTime fechaNac = DateTime.MinValue;
                if (!string.IsNullOrEmpty(txtFechaNacimiento.Text))
                    DateTime.TryParse(txtFechaNacimiento.Text, out fechaNac);

                Autor nuevoAutor = new Autor(
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtNacionalidad.Text.Trim(),
                    fechaNac
                );

                nuevoAutor.Save_Autor();
                LimpiarFormulario();
                CargarAutores();
            }
        }

        protected void gvAutores_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNacionalidad.Text = "";
            txtFechaNacimiento.Text = "";
        }
    }
}
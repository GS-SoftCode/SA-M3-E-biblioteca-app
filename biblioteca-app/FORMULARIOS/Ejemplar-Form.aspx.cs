using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Ejemplar_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarLibros();
                CargarEjemplares();
            }
        }

        private void CargarLibros()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_libros");
            ddlLibro.DataSource = dt;
            ddlLibro.DataTextField = "titulo";
            ddlLibro.DataValueField = "id_libro";
            ddlLibro.DataBind();
            ddlLibro.Items.Insert(0, new ListItem("-- Seleccione un libro --", "0"));
        }

        private void CargarEjemplares()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_ejemplares");
            gvEjemplares.DataSource = dt;
            gvEjemplares.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Determinar disponibilidad según el estado
                string estado = ddlEstado.SelectedValue;
                bool disponible = (estado == "Disponible");

                Ejemplar nuevoEjemplar = new Ejemplar(
                    int.Parse(ddlLibro.SelectedValue),
                    estado,
                    txtUbicacion.Text.Trim(),
                    disponible
                );

                nuevoEjemplar.Save_Ejemplar();
                LimpiarFormulario();
                CargarEjemplares();
            }
        }

        protected void gvEjemplares_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            ddlLibro.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;
            txtUbicacion.Text = "";
        }
    }
}
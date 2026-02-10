using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Libro_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarAutores();
                CargarLibros();
            }
        }

        private void CargarCategorias()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_categorias");
            ddlCategoria.DataSource = dt;
            ddlCategoria.DataTextField = "nombre";
            ddlCategoria.DataValueField = "id_categoria";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione una categoría --", "0"));
        }

        private void CargarAutores()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_autores");
            ddlAutor.DataSource = dt;
            ddlAutor.DataTextField = "nombre_completo";
            ddlAutor.DataValueField = "id_autor";
            ddlAutor.DataBind();
            ddlAutor.Items.Insert(0, new ListItem("-- Seleccione un autor --", "0"));
        }

        private void CargarLibros()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_libros");
            gvLibros.DataSource = dt;
            gvLibros.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int anio = 0;
                int.TryParse(txtAnioPublicacion.Text.Trim(), out anio);

                Libro nuevoLibro = new Libro(
                    txtTitulo.Text.Trim(),
                    txtIsbn.Text.Trim(),
                    txtEditorial.Text.Trim(),
                    anio,
                    int.Parse(ddlCategoria.SelectedValue),
                    int.Parse(ddlAutor.SelectedValue)
                );

                nuevoLibro.Save_Libro();
                LimpiarFormulario();
                CargarLibros();
            }
        }

        protected void gvLibros_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            txtTitulo.Text = "";
            txtIsbn.Text = "";
            txtEditorial.Text = "";
            txtAnioPublicacion.Text = "";
            ddlCategoria.SelectedIndex = 0;
            ddlAutor.SelectedIndex = 0;
        }
    }
}
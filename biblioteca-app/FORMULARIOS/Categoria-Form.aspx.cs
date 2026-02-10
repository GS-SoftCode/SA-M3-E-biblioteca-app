using System;
using System.Data;
using System.Web.UI;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Categoria_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarCategorias();
        }

        private void CargarCategorias()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_categorias");
            gvCategorias.DataSource = dt;
            gvCategorias.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Categoria nuevaCategoria = new Categoria(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                nuevaCategoria.Save_Categoria();
                LimpiarFormulario();
                CargarCategorias();
            }
        }

        protected void gvCategorias_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }
    }
}
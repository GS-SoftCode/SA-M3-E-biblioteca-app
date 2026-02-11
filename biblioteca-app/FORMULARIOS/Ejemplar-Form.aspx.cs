using System;
using System.Data;
using System.Data.SqlClient;
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && !string.IsNullOrEmpty(hfIdEjemplar.Value) && hfIdEjemplar.Value != "0")
            {
                string estado = ddlEstado.SelectedValue;
                bool disponible = (estado == "Disponible");

                SqlParameter[] Parametros = new SqlParameter[5];
                Parametros[0] = new SqlParameter("@id_ejemplar", SqlDbType.Int) { Value = int.Parse(hfIdEjemplar.Value) };
                Parametros[1] = new SqlParameter("@id_libro", SqlDbType.Int) { Value = int.Parse(ddlLibro.SelectedValue) };
                Parametros[2] = new SqlParameter("@estado", SqlDbType.NVarChar, 20) { Value = estado };
                Parametros[3] = new SqlParameter("@ubicacion", SqlDbType.NVarChar, 100) { Value = string.IsNullOrEmpty(txtUbicacion.Text) ? (object)DBNull.Value : txtUbicacion.Text.Trim() };
                Parametros[4] = new SqlParameter("@disponible", SqlDbType.Bit) { Value = disponible };

                SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "update_ejemplar", Parametros);
                
                LimpiarFormulario();
                CargarEjemplares();
                ModoInsertar();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            ModoInsertar();
        }

        protected void gvEjemplares_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEjemplar = Convert.ToInt32(gvEjemplares.SelectedDataKey.Value);
            CargarEjemplarParaEdicion(idEjemplar);
            ModoEdicion();
        }

        private void CargarEjemplarParaEdicion(int idEjemplar)
        {
            SqlParameter[] Parametros = new SqlParameter[1];
            Parametros[0] = new SqlParameter("@id_ejemplar", SqlDbType.Int) { Value = idEjemplar };

            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_ejemplar_by_id", Parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                hfIdEjemplar.Value = row["id_ejemplar"].ToString();
                ddlLibro.SelectedValue = row["id_libro"].ToString();
                ddlEstado.SelectedValue = row["estado"].ToString();
                txtUbicacion.Text = row["ubicacion"].ToString();
            }
        }

        private void ModoEdicion()
        {
            btnGuardar.Visible = false;
            btnActualizar.Visible = true;
            btnCancelar.Visible = true;
        }

        private void ModoInsertar()
        {
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
            btnCancelar.Visible = false;
            hfIdEjemplar.Value = "0";
        }

        private void LimpiarFormulario()
        {
            ddlLibro.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;
            txtUbicacion.Text = "";
            hfIdEjemplar.Value = "0";
        }
    }
}
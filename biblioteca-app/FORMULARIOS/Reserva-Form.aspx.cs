using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using biblioteca_app.CLASES;

namespace biblioteca_app
{
    public partial class Reserva_Form : Page
    {
        private string connectionString = "Data Source=(local);Initial Catalog=biblioteca_db;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarEjemplares();
                CargarReservas();
            }
        }

        private void CargarUsuarios()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_usuarios");
            
            // Agregar una columna calculada para nombre completo
            dt.Columns.Add("nombre_completo", typeof(string), "nombre + ' ' + apellido");
            
            ddlUsuario.DataSource = dt;
            ddlUsuario.DataTextField = "nombre_completo";
            ddlUsuario.DataValueField = "id_usuario";
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("-- Seleccione un usuario --", "0"));
        }

        private void CargarEjemplares()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_ejemplares");
            
            // Filtrar solo los ejemplares NO disponibles (para poder reservarlos)
            DataView dv = dt.DefaultView;
            dv.RowFilter = "estado = 'No disponible'";
            DataTable dtFiltrado = dv.ToTable();
            
            // Agregar columna calculada para mostrar: "Título - ID"
            dtFiltrado.Columns.Add("display_text", typeof(string), "titulo_libro + ' - ' + id_ejemplar");
            
            ddlEjemplar.DataSource = dtFiltrado;
            ddlEjemplar.DataTextField = "display_text";
            ddlEjemplar.DataValueField = "id_ejemplar";
            ddlEjemplar.DataBind();
            ddlEjemplar.Items.Insert(0, new ListItem("-- Seleccione un ejemplar --", "0"));
        }

        private void CargarReservas()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_reservas");
            gvReservas.DataSource = dt;
            gvReservas.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DateTime fechaFin = DateTime.MinValue;
                if (!string.IsNullOrEmpty(txtFechaFin.Text))
                    DateTime.TryParse(txtFechaFin.Text, out fechaFin);

                Reserva nuevaReserva = new Reserva(
                    int.Parse(ddlUsuario.SelectedValue),
                    int.Parse(ddlEjemplar.SelectedValue),
                    fechaFin,
                    ddlEstado.SelectedValue
                );

                nuevaReserva.Save_Reserva();
                LimpiarFormulario();
                CargarEjemplares();  // Recargar lista de ejemplares disponibles
                CargarReservas();
            }
        }

        protected void gvReservas_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            ddlUsuario.SelectedIndex = 0;
            ddlEjemplar.SelectedIndex = 0;
            txtFechaFin.Text = "";
            ddlEstado.SelectedIndex = 0;
        }
    }
}
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
                CargarReservasActivas();
                CargarPrestamos();
            }
        }

        private void CargarUsuarios()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_usuarios");
            
            // Agregar columna calculada para nombre completo
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
            
            // Filtrar solo los ejemplares disponibles
            DataView dv = dt.DefaultView;
            dv.RowFilter = "disponible = true AND estado = 'Disponible'";
            DataTable dtFiltrado = dv.ToTable();
            
            // Agregar columna calculada para mostrar: "Título - ID"
            dtFiltrado.Columns.Add("display_text", typeof(string), "titulo_libro + ' - ' + id_ejemplar");
            
            ddlEjemplar.DataSource = dtFiltrado;
            ddlEjemplar.DataTextField = "display_text";
            ddlEjemplar.DataValueField = "id_ejemplar";
            ddlEjemplar.DataBind();
            ddlEjemplar.Items.Insert(0, new ListItem("-- Seleccione un ejemplar --", "0"));
        }

        private void CargarReservasActivas()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_reservas_activas");
            
            if (dt.Rows.Count > 0)
            {
                // Agregar columna calculada: "Reserva #X - Nombre Apellido - Libro - ID"
                dt.Columns.Add("display_text", typeof(string), 
                    "'Reserva #' + CONVERT(id_reserva, 'System.String') + ' - ' + nombre_usuario + ' - ' + titulo_libro + ' - ' + CONVERT(id_ejemplar, 'System.String')");
                
                ddlReservas.DataSource = dt;
                ddlReservas.DataTextField = "display_text";
                ddlReservas.DataValueField = "id_reserva";
                ddlReservas.DataBind();
            }
            
            ddlReservas.Items.Insert(0, new ListItem("-- Seleccione una reserva --", "0"));
        }

        private void CargarPrestamos()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_prestamos");
            gvPrestamos.DataSource = dt;
            gvPrestamos.DataBind();
        }

        protected void ddlReservas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReservas.SelectedValue != "0")
            {
                // Obtener datos de la reserva seleccionada
                DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_reservas_activas");
                DataView dv = dt.DefaultView;
                dv.RowFilter = "id_reserva = " + ddlReservas.SelectedValue;
                
                if (dv.Count > 0)
                {
                    DataRow row = dv[0].Row;
                    
                    // Cargar datos en los campos del formulario
                    ddlUsuario.SelectedValue = row["id_usuario"].ToString();
                    
                    // Recargar ejemplares para incluir el de la reserva (que está "No disponible")
                    CargarEjemplaresConReserva(Convert.ToInt32(row["id_ejemplar"]));
                    ddlEjemplar.SelectedValue = row["id_ejemplar"].ToString();
                    
                    // Establecer fecha límite (7 días desde hoy por defecto)
                    txtFechaLimite.Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                }
            }
        }

        private void CargarEjemplaresConReserva(int idEjemplarReserva)
        {
            DataTable dt = SqlHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, "get_ejemplares");
            
            // Filtrar disponibles O el ejemplar de la reserva
            DataView dv = dt.DefaultView;
            dv.RowFilter = "(disponible = true AND estado = 'Disponible') OR id_ejemplar = " + idEjemplarReserva;
            DataTable dtFiltrado = dv.ToTable();
            
            // Agregar columna calculada
            dtFiltrado.Columns.Add("display_text", typeof(string), "titulo_libro + ' - ' + id_ejemplar");
            
            ddlEjemplar.DataSource = dtFiltrado;
            ddlEjemplar.DataTextField = "display_text";
            ddlEjemplar.DataValueField = "id_ejemplar";
            ddlEjemplar.DataBind();
            ddlEjemplar.Items.Insert(0, new ListItem("-- Seleccione un ejemplar --", "0"));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DateTime fechaLimite = DateTime.MinValue;
                if (!string.IsNullOrEmpty(txtFechaLimite.Text))
                    DateTime.TryParse(txtFechaLimite.Text, out fechaLimite);

                // Determinar qué campo de observaciones usar
                string observaciones = "";
                if (ddlReservas.SelectedValue != "0")
                {
                    // Si viene de reserva, usar el campo txtObservacionesReserva
                    observaciones = txtObservacionesReserva.Text.Trim();
                }
                else
                {
                    // Si es préstamo manual, usar el campo txtObservaciones
                    observaciones = txtObservaciones.Text.Trim();
                }

                Prestamo nuevoPrestamo = new Prestamo(
                    int.Parse(ddlUsuario.SelectedValue),
                    fechaLimite,
                    observaciones,
                    int.Parse(ddlEjemplar.SelectedValue)
                );

                nuevoPrestamo.Save_Prestamo();
                
                // Si se creó desde una reserva, actualizar el estado de la reserva a "Finalizada"
                if (ddlReservas.SelectedValue != "0")
                {
                    ActualizarEstadoReserva(int.Parse(ddlReservas.SelectedValue), "Finalizada");
                }
                
                LimpiarFormulario();
                CargarEjemplares();
                CargarReservasActivas();
                CargarPrestamos();
            }
        }

        private void ActualizarEstadoReserva(int idReserva, string nuevoEstado)
        {
            System.Data.SqlClient.SqlParameter[] Parametros = new System.Data.SqlClient.SqlParameter[2];
            Parametros[0] = new System.Data.SqlClient.SqlParameter("@id_reserva", SqlDbType.Int) { Value = idReserva };
            Parametros[1] = new System.Data.SqlClient.SqlParameter("@nuevo_estado", SqlDbType.NVarChar, 20) { Value = nuevoEstado };
            
            SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "update_estado_reserva", Parametros);
        }

        protected void gvPrestamos_SelectedIndexChanged(object sender, EventArgs e) { }

        private void LimpiarFormulario()
        {
            ddlUsuario.SelectedIndex = 0;
            ddlEjemplar.SelectedIndex = 0;
            txtFechaLimite.Text = "";
            txtObservaciones.Text = "";
            txtObservacionesReserva.Text = "";
            ddlReservas.SelectedIndex = 0;
        }
    }
}
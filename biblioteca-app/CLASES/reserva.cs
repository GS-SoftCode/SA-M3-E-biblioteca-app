using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace biblioteca_app.CLASES
{
    public class Reserva
    {
        // Propiedades privadas
        int idUsuario;
        int idEjemplar;
        DateTime fechaFin;
        string estado;

        // Propiedades públicas con get y set
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int IdEjemplar { get => idEjemplar; set => idEjemplar = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public string Estado { get => estado; set => estado = value; }

        // Constructor
        public Reserva(int auxIdUsuario, int auxIdEjemplar, DateTime auxFechaFin, string auxEstado)
        {
            IdUsuario = auxIdUsuario;
            IdEjemplar = auxIdEjemplar;
            FechaFin = auxFechaFin;
            Estado = auxEstado;
        }

        // Método para guardar reserva en BD usando procedimiento almacenado
        public int Save_Reserva()
        {
            var connSetting = WebConfigurationManager.ConnectionStrings["biblioteca_db_connection"];
            string connString = connSetting.ConnectionString;

            // Preparar parámetros (4)
            SqlParameter[] Parametros = new SqlParameter[4];
            Parametros[0] = new SqlParameter("@id_usuario", SqlDbType.Int) { Value = IdUsuario };
            Parametros[1] = new SqlParameter("@id_ejemplar", SqlDbType.Int) { Value = IdEjemplar };
            Parametros[2] = new SqlParameter("@fecha_fin", SqlDbType.DateTime) { Value = FechaFin };
            Parametros[3] = new SqlParameter("@estado", SqlDbType.NVarChar, 20) { Value = string.IsNullOrEmpty(Estado) ? "Activa" : Estado };

            // Ejecutar el stored procedure usando SqlHelper
            object resultado = SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "save_reserva", Parametros);
            
            int idReserva = resultado != null ? Convert.ToInt32(resultado) : 0;
            
            return idReserva;
        }
    }
}

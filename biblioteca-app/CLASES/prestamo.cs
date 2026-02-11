using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace biblioteca_app.CLASES
{
    public class Prestamo
    {
        // Propiedades privadas
        int idUsuario;
        DateTime fechaLimite;
        string observaciones;
        int idEjemplar;

        // Propiedades públicas con get y set
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public DateTime FechaLimite { get => fechaLimite; set => fechaLimite = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public int IdEjemplar { get => idEjemplar; set => idEjemplar = value; }

        // Constructor
        public Prestamo(int auxIdUsuario, DateTime auxFechaLimite, string auxObservaciones, int auxIdEjemplar)
        {
            IdUsuario = auxIdUsuario;
            FechaLimite = auxFechaLimite;
            Observaciones = auxObservaciones;
            IdEjemplar = auxIdEjemplar;
        }

        // Método para guardar préstamo en BD usando procedimiento almacenado
        public int Save_Prestamo()
        {
            var connSetting = WebConfigurationManager.ConnectionStrings["biblioteca_db_connection"];
            string connString = connSetting.ConnectionString;

            // Preparar parámetros (4)
            SqlParameter[] Parametros = new SqlParameter[4];
            Parametros[0] = new SqlParameter("@id_usuario", SqlDbType.Int) { Value = IdUsuario };
            Parametros[1] = new SqlParameter("@fecha_limite", SqlDbType.DateTime) { Value = FechaLimite };
            Parametros[2] = new SqlParameter("@observaciones", SqlDbType.NVarChar, 200) { Value = string.IsNullOrEmpty(Observaciones) ? (object)DBNull.Value : Observaciones };
            Parametros[3] = new SqlParameter("@id_ejemplar", SqlDbType.Int) { Value = IdEjemplar };

            // Ejecutar el stored procedure usando SqlHelper
            object resultado = SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "save_prestamo", Parametros);
            
            int idPrestamo = resultado != null ? Convert.ToInt32(resultado) : 0;
            
            // Si el préstamo se guardó exitosamente, actualizar el estado del ejemplar
            if (idPrestamo > 0)
            {
                ActualizarEstadoEjemplar(connString, IdEjemplar, "No disponible");
            }
            
            return idPrestamo;
        }
        
        // Método privado para actualizar el estado del ejemplar
        private void ActualizarEstadoEjemplar(string connString, int idEjemplar, string nuevoEstado)
        {
            SqlParameter[] Parametros = new SqlParameter[2];
            Parametros[0] = new SqlParameter("@id_ejemplar", SqlDbType.Int) { Value = idEjemplar };
            Parametros[1] = new SqlParameter("@nuevo_estado", SqlDbType.NVarChar, 20) { Value = nuevoEstado };
            
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "update_estado_ejemplar", Parametros);
        }
    }
}

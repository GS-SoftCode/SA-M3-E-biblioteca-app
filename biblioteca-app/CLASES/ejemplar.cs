using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace biblioteca_app.CLASES
{
    public class Ejemplar
    {
        // Propiedades privadas
        int idLibro;
        string estado;
        string ubicacion;
        bool disponible;

        // Propiedades públicas con get y set
        public int IdLibro { get => idLibro; set => idLibro = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }
        public bool Disponible { get => disponible; set => disponible = value; }

        // Constructor
        public Ejemplar(int auxIdLibro, string auxEstado, string auxUbicacion, bool auxDisponible)
        {
            IdLibro = auxIdLibro;
            Estado = auxEstado;
            Ubicacion = auxUbicacion;
            Disponible = auxDisponible;
        }

        // Método para guardar ejemplar en BD usando procedimiento almacenado
        public int Save_Ejemplar()
        {
            var connSetting = WebConfigurationManager.ConnectionStrings["biblioteca_db_connection"];
            string connString = connSetting.ConnectionString;

            // Preparar parámetros (4)
            SqlParameter[] Parametros = new SqlParameter[4];
            Parametros[0] = new SqlParameter("@id_libro", SqlDbType.Int) { Value = IdLibro };
            Parametros[1] = new SqlParameter("@estado", SqlDbType.NVarChar, 20) { Value = string.IsNullOrEmpty(Estado) ? "Disponible" : Estado };
            Parametros[2] = new SqlParameter("@ubicacion", SqlDbType.NVarChar, 100) { Value = string.IsNullOrEmpty(Ubicacion) ? (object)DBNull.Value : Ubicacion };
            Parametros[3] = new SqlParameter("@disponible", SqlDbType.Bit) { Value = Disponible };

            // Ejecutar el stored procedure usando SqlHelper
            object resultado = SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "save_ejemplar", Parametros);
            return resultado != null ? Convert.ToInt32(resultado) : 0;
        }
    }
}

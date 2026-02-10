using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace biblioteca_app.CLASES
{
    public class Rol
    {
        // Propiedades privadas
        string nombre;
        string descripcion;

        // Propiedades públicas con get y set
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        // Constructor
        public Rol(string auxNombre, string auxDescripcion)
        {
            Nombre = auxNombre;
            Descripcion = auxDescripcion;
        }

        // Método para guardar rol en BD usando procedimiento almacenado
        public int Save_Rol()
        {
            var connSetting = WebConfigurationManager.ConnectionStrings["biblioteca_db_connection"];
            string connString = connSetting.ConnectionString;

            // Preparar parámetros (2)
            SqlParameter[] Parametros = new SqlParameter[2];
            Parametros[0] = new SqlParameter("@nombre", SqlDbType.NVarChar, 50) { Value = (object)Nombre ?? DBNull.Value };
            Parametros[1] = new SqlParameter("@descripcion", SqlDbType.NVarChar, 200) { Value = string.IsNullOrEmpty(Descripcion) ? (object)DBNull.Value : Descripcion };

            // Ejecutar el stored procedure usando SqlHelper
            object resultado = SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "save_rol", Parametros);
            return resultado != null ? Convert.ToInt32(resultado) : 0;
        }
    }
}
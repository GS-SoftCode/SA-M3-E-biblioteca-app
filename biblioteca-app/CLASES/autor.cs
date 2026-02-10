using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace biblioteca_app.CLASES
{
    public class Autor
    {
        // Propiedades privadas
        string nombre;
        string apellido;
        string nacionalidad;
        DateTime fechaNacimiento;

        // Propiedades públicas con get y set
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Nacionalidad { get => nacionalidad; set => nacionalidad = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }

        // Constructor
        public Autor(string auxNombre, string auxApellido, string auxNacionalidad, DateTime auxFechaNacimiento)
        {
            Nombre = auxNombre;
            Apellido = auxApellido;
            Nacionalidad = auxNacionalidad;
            FechaNacimiento = auxFechaNacimiento;
        }

        // Método para guardar autor en BD usando procedimiento almacenado
        public int Save_Autor()
        {
            var connSetting = WebConfigurationManager.ConnectionStrings["biblioteca_db_connection"];
            string connString = connSetting.ConnectionString;

            // Preparar parámetros (4)
            SqlParameter[] Parametros = new SqlParameter[4];
            Parametros[0] = new SqlParameter("@nombre", SqlDbType.NVarChar, 100) { Value = (object)Nombre ?? DBNull.Value };
            Parametros[1] = new SqlParameter("@apellido", SqlDbType.NVarChar, 100) { Value = (object)Apellido ?? DBNull.Value };
            Parametros[2] = new SqlParameter("@nacionalidad", SqlDbType.NVarChar, 50) { Value = string.IsNullOrEmpty(Nacionalidad) ? (object)DBNull.Value : Nacionalidad };
            Parametros[3] = new SqlParameter("@fecha_nacimiento", SqlDbType.Date) { Value = FechaNacimiento != DateTime.MinValue ? (object)FechaNacimiento : DBNull.Value };

            // Ejecutar el stored procedure usando SqlHelper
            object resultado = SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "save_autor", Parametros);
            return resultado != null ? Convert.ToInt32(resultado) : 0;
        }
    }
}

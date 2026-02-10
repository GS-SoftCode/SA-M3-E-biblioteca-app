using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace biblioteca_app.CLASES
{
    public class Usuario
    {
        // Propiedades privadas
        string nombre;
        string apellido;
        string email;
        string telefono;
        string direccion;
        int idRol;

        // Propiedades públicas con get y set
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Email { get => email; set => email = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int IdRol { get => idRol; set => idRol = value; }

        // Constructor
        public Usuario(string auxNombre, string auxApellido, string auxEmail, string auxTelefono, string auxDireccion, int auxIdRol)
        {
            Nombre = auxNombre;
            Apellido = auxApellido;
            Email = auxEmail;
            Telefono = auxTelefono;
            Direccion = auxDireccion;
            IdRol = auxIdRol;
        }

        // Método para guardar usuario en BD usando procedimiento almacenado
        public int Save_Usuario()
        {
            var connSetting = WebConfigurationManager.ConnectionStrings["biblioteca_db_connection"];
            string connString = connSetting.ConnectionString;

            // Preparar parámetros (6)
            SqlParameter[] Parametros = new SqlParameter[6];
            Parametros[0] = new SqlParameter("@nombre", SqlDbType.NVarChar, 100) { Value = (object)Nombre ?? DBNull.Value };
            Parametros[1] = new SqlParameter("@apellido", SqlDbType.NVarChar, 100) { Value = (object)Apellido ?? DBNull.Value };
            Parametros[2] = new SqlParameter("@email", SqlDbType.NVarChar, 100) { Value = (object)Email ?? DBNull.Value };
            Parametros[3] = new SqlParameter("@telefono", SqlDbType.NVarChar, 20) { Value = string.IsNullOrEmpty(Telefono) ? (object)DBNull.Value : Telefono };
            Parametros[4] = new SqlParameter("@direccion", SqlDbType.NVarChar, 200) { Value = string.IsNullOrEmpty(Direccion) ? (object)DBNull.Value : Direccion };
            Parametros[5] = new SqlParameter("@id_rol", SqlDbType.Int) { Value = IdRol };

            // Ejecutar el stored procedure usando SqlHelper
            object resultado = SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "save_usuario", Parametros);
            return resultado != null ? Convert.ToInt32(resultado) : 0;
        }
    }
}
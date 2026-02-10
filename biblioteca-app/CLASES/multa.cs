using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace biblioteca_app.CLASES
{
    public class Multa
    {
        // Propiedades privadas
        string tipoMulta;
        decimal monto;
        string descripcion;
        string estado;
        int idUsuario;
        int idPrestamo;

        // Propiedades públicas con get y set
        public string TipoMulta { get => tipoMulta; set => tipoMulta = value; }
        public decimal Monto { get => monto; set => monto = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Estado { get => estado; set => estado = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int IdPrestamo { get => idPrestamo; set => idPrestamo = value; }

        // Constructor
        public Multa(string auxTipoMulta, decimal auxMonto, string auxDescripcion, string auxEstado, int auxIdUsuario, int auxIdPrestamo)
        {
            TipoMulta = auxTipoMulta;
            Monto = auxMonto;
            Descripcion = auxDescripcion;
            Estado = auxEstado;
            IdUsuario = auxIdUsuario;
            IdPrestamo = auxIdPrestamo;
        }

        // Método para guardar multa en BD usando procedimiento almacenado
        public int Save_Multa()
        {
            var connSetting = WebConfigurationManager.ConnectionStrings["biblioteca_db_connection"];
            string connString = connSetting.ConnectionString;

            // Preparar parámetros (6)
            SqlParameter[] Parametros = new SqlParameter[6];
            Parametros[0] = new SqlParameter("@tipo_multa", SqlDbType.NVarChar, 100) { Value = (object)TipoMulta ?? DBNull.Value };
            Parametros[1] = new SqlParameter("@monto", SqlDbType.Decimal) { Value = Monto, Precision = 10, Scale = 2 };
            Parametros[2] = new SqlParameter("@descripcion", SqlDbType.NVarChar, 200) { Value = string.IsNullOrEmpty(Descripcion) ? (object)DBNull.Value : Descripcion };
            Parametros[3] = new SqlParameter("@estado", SqlDbType.NVarChar, 10) { Value = string.IsNullOrEmpty(Estado) ? "No pagado" : Estado };
            Parametros[4] = new SqlParameter("@id_usuario", SqlDbType.Int) { Value = IdUsuario };
            Parametros[5] = new SqlParameter("@id_prestamo", SqlDbType.Int) { Value = IdPrestamo };

            // Ejecutar el stored procedure usando SqlHelper
            object resultado = SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "save_multa", Parametros);
            return resultado != null ? Convert.ToInt32(resultado) : 0;
        }
    }
}

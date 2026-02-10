using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace biblioteca_app.CLASES
{
    public class Libro
    {
        // Propiedades privadas
        string titulo;
        string isbn;
        string editorial;
        int anioPublicacion;
        int idCategoria;
        int idAutor;

        // Propiedades públicas con get y set
        public string Titulo { get => titulo; set => titulo = value; }
        public string Isbn { get => isbn; set => isbn = value; }
        public string Editorial { get => editorial; set => editorial = value; }
        public int AnioPublicacion { get => anioPublicacion; set => anioPublicacion = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public int IdAutor { get => idAutor; set => idAutor = value; }

        // Constructor
        public Libro(string auxTitulo, string auxIsbn, string auxEditorial, int auxAnioPublicacion, int auxIdCategoria, int auxIdAutor)
        {
            Titulo = auxTitulo;
            Isbn = auxIsbn;
            Editorial = auxEditorial;
            AnioPublicacion = auxAnioPublicacion;
            IdCategoria = auxIdCategoria;
            IdAutor = auxIdAutor;
        }

        // Método para guardar libro en BD usando procedimiento almacenado
        public int Save_Libro()
        {
            var connSetting = WebConfigurationManager.ConnectionStrings["biblioteca_db_connection"];
            string connString = connSetting.ConnectionString;

            // Preparar parámetros (6)
            SqlParameter[] Parametros = new SqlParameter[6];
            Parametros[0] = new SqlParameter("@titulo", SqlDbType.NVarChar, 200) { Value = (object)Titulo ?? DBNull.Value };
            Parametros[1] = new SqlParameter("@isbn", SqlDbType.NVarChar, 20) { Value = (object)Isbn ?? DBNull.Value };
            Parametros[2] = new SqlParameter("@editorial", SqlDbType.NVarChar, 100) { Value = string.IsNullOrEmpty(Editorial) ? (object)DBNull.Value : Editorial };
            Parametros[3] = new SqlParameter("@anio_publicacion", SqlDbType.Int) { Value = AnioPublicacion > 0 ? (object)AnioPublicacion : DBNull.Value };
            Parametros[4] = new SqlParameter("@id_categoria", SqlDbType.Int) { Value = IdCategoria };
            Parametros[5] = new SqlParameter("@id_autor", SqlDbType.Int) { Value = IdAutor };

            // Ejecutar el stored procedure usando SqlHelper
            object resultado = SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "save_libro", Parametros);
            return resultado != null ? Convert.ToInt32(resultado) : 0;
        }
    }
}

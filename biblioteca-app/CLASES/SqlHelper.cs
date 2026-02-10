using System;
using System.Data;
using System.Data.SqlClient;

namespace biblioteca_app.CLASES
{
    public sealed class SqlHelper
    {
        // Constructor privado para prevenir instanciación
        private SqlHelper() { }

        // Ejecutar comando sin retornar datos (INSERT, UPDATE, DELETE)
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    if (commandParameters != null)
                        cmd.Parameters.AddRange(commandParameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Ejecutar comando y retornar un valor escalar (SELECT que retorna un solo valor)
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    if (commandParameters != null)
                        cmd.Parameters.AddRange(commandParameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        // Ejecutar comando y retornar un DataReader
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(commandText, conn);
                cmd.CommandType = commandType;
                if (commandParameters != null)
                    cmd.Parameters.AddRange(commandParameters);

                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        // Ejecutar comando y retornar un DataSet
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    if (commandParameters != null)
                        cmd.Parameters.AddRange(commandParameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        // Ejecutar comando y retornar un DataTable
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    if (commandParameters != null)
                        cmd.Parameters.AddRange(commandParameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}

using System;
using System.Data.SqlClient;

namespace Proyecto.Capa_Datos
{
    internal class Conexion : IDisposable
    {
        private readonly string connectionString;
        private SqlConnection sqlConnection;

        // Propiedad pública para acceder a la cadena de conexión
        public string ConnectionString
        {
            get { return connectionString; }
        }

        public SqlConnection GetSqlConnection()
        {
            // Devuelve la conexión subyacente
            return sqlConnection;
        }

        public Conexion()
        {
            connectionString = "Data Source=DESKTOP-5AKJRSL;Initial Catalog=Test_Invoices;Integrated Security=True;";
        }

        public void OpenConnection()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(connectionString);
            }

            if (sqlConnection.State != System.Data.ConnectionState.Open)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public void Dispose()
        {
            CloseConnection();
            sqlConnection.Dispose();
        }
    }
}

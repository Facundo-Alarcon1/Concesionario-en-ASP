using System;
using System.Data.SqlClient;
using System.Configuration; // Para leer del web.config

namespace ConcesionarioWEBFORM1111.DataBase
{
    public class DataBaseConnection
    {
        public string connectionString;

        public DataBaseConnection()
        {
            // Lee la cadena de conexión desde el archivo web.config
            connectionString = ConfigurationManager.ConnectionStrings["ConcesionariaConnection"].ConnectionString;
        }

        public void Connect()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                        Console.WriteLine("Conexión cerrada.");
                    }
                }
            }
        }
    }
}


using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model.Utilities;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public class LoginDAO : ILoginDAO
    {
        private readonly DataBaseConnection dbConnection;

        public LoginDAO(DataBaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<(string Puesto, int IdEmpleado, string NombreUsuario, string ContraseniaHash)> ObtenerUsuarioPorNombreAsync(string nombreUsuario)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT Puesto, ID_empleado, NombreUsuario, Contraseña FROM Empleados WHERE NombreUsuario = @NombreUsuario";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string puesto = reader["Puesto"].ToString();
                                int idEmpleado = Convert.ToInt32(reader["ID_empleado"]);
                                string nombreUsuarioDb = reader["NombreUsuario"].ToString();
                                string contraseniaDb = reader["Contraseña"].ToString();
                                
                                return (puesto, idEmpleado, nombreUsuarioDb, contraseniaDb);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al obtener usuario: {ex.Message}", ex);
                }
            }
            return (null, 0, null, null);
        }
    }
}


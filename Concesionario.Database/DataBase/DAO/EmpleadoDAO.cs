using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;
using ConcesionarioWEBFORM1111.Model.Utilities;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public class EmpleadoDAO : IEmpleadoDAO
    {
        private readonly DataBaseConnection dbConnection;

        public EmpleadoDAO(DataBaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<List<Empleados>> ObtenerEmpleadosAsync()
        {
            List<Empleados> empleados = new List<Empleados>();
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT ID_empleado, Nombre, Apellido, Puesto FROM Empleados";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                empleados.Add(new Empleados
                                {
                                    ID_empleado = Convert.ToInt32(reader["ID_empleado"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Puesto = reader["Puesto"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error al obtener empleados: " + ex.Message, ex);
            }
            return empleados;
        }

        public async Task<bool> AgregarEmpleadoAsync(Empleados empleado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO Empleados (Nombre, Apellido, Puesto) VALUES (@Nombre, @Apellido, @Puesto)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                        cmd.Parameters.AddWithValue("@Puesto", empleado.Puesto);
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error al agregar empleado: " + ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> ModificarEmpleadoAsync(Empleados empleado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE Empleados SET Nombre = @Nombre, Apellido = @Apellido, Puesto = @Puesto WHERE ID_empleado = @ID_empleado";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                        cmd.Parameters.AddWithValue("@Puesto", empleado.Puesto);
                        cmd.Parameters.AddWithValue("@ID_empleado", empleado.ID_empleado);
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error al modificar empleado: " + ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> EliminarEmpleadoAsync(int idEmpleado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    await conn.OpenAsync();
                    string query = "DELETE FROM Empleados WHERE ID_empleado = @ID_empleado";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_empleado", idEmpleado);
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error al eliminar empleado: " + ex.Message, ex);
                return false;
            }
        }
    }
}

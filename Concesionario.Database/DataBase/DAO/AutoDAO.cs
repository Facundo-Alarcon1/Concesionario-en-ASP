using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;
using ConcesionarioWEBFORM1111.Model.Utilities;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public class AutoDAO : IAutoDAO
    {
        private readonly DataBaseConnection dbConnection;

        public AutoDAO(DataBaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<bool> EmpleadoExisteAsync(int idEmpleado)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT COUNT(*) FROM Empleados WHERE ID_empleado = @ID_empleado";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_empleado", idEmpleado);
                        int empleadoExiste = (int)await command.ExecuteScalarAsync();
                        return empleadoExiste > 0;
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al verificar empleado: {ex.Message}", ex);
                    return false;
                }
            }
        }

        public async Task<bool> AgregarAutoYComprobanteAsync(Auto auto)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string queryAuto = "INSERT INTO Auto (Marca, Modelo, Color, Patente, Anio, Estado, ID_empleado, Precio) " +
                                       "VALUES (@Marca, @Modelo, @Color, @Patente, @Anio, 'disponible', @ID_empleado, @Precio); " +
                                       "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    using (SqlCommand commandAuto = new SqlCommand(queryAuto, connection))
                    {
                        commandAuto.Parameters.AddWithValue("@Marca", auto.Marca);
                        commandAuto.Parameters.AddWithValue("@Modelo", auto.Modelo);
                        commandAuto.Parameters.AddWithValue("@Color", auto.Color);
                        commandAuto.Parameters.AddWithValue("@Patente", auto.Patente);
                        commandAuto.Parameters.AddWithValue("@Anio", auto.Anio);
                        commandAuto.Parameters.AddWithValue("@ID_empleado", auto.ID_empleado);
                        commandAuto.Parameters.AddWithValue("@Precio", auto.Precio);

                        object result = await commandAuto.ExecuteScalarAsync();
                        int idNuevoAuto = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                        if (idNuevoAuto > 0)
                        {
                            string queryComprobante = "INSERT INTO Comprobante (Tipo, FechaHora, ID_auto, ID_empleado, Estado, Observaciones, Precio) " +
                                                      "VALUES ('compra', GETDATE(), @ID_auto, @ID_empleado, 'disponible', @Observaciones, @Precio)";
                            using (SqlCommand commandComprobante = new SqlCommand(queryComprobante, connection))
                            {
                                commandComprobante.Parameters.AddWithValue("@ID_auto", idNuevoAuto);
                                commandComprobante.Parameters.AddWithValue("@ID_empleado", auto.ID_empleado);
                                commandComprobante.Parameters.AddWithValue("@Observaciones", "Auto ingresado al inventario");
                                commandComprobante.Parameters.AddWithValue("@Precio", auto.Precio);

                                int rowsAffectedComprobante = await commandComprobante.ExecuteNonQueryAsync();
                                return rowsAffectedComprobante > 0;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al agregar el auto: {ex.Message}", ex);
                }
            }
            return false;
        }

        public async Task<bool> ActualizarCampoAutoAsync(int idAuto, string campo, object nuevoValor)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = $"UPDATE Auto SET {campo} = @Valor WHERE ID_auto = @ID_auto";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_auto", idAuto);
                        command.Parameters.AddWithValue("@Valor", nuevoValor);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al actualizar {campo} del auto: {ex.Message}", ex);
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> ModificarAutoAsync(Auto auto)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "UPDATE Auto SET Marca = @Marca, Modelo = @Modelo, Color = @Color, " +
                                   "Patente = @Patente, Anio = @Anio, Precio = @Precio WHERE ID_auto = @ID_auto";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Marca", auto.Marca);
                        command.Parameters.AddWithValue("@Modelo", auto.Modelo);
                        command.Parameters.AddWithValue("@Color", auto.Color);
                        command.Parameters.AddWithValue("@Patente", auto.Patente);
                        command.Parameters.AddWithValue("@Anio", auto.Anio);
                        command.Parameters.AddWithValue("@Precio", auto.Precio);
                        command.Parameters.AddWithValue("@ID_auto", auto.ID_auto);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al modificar el auto: {ex.Message}", ex);
                    return false;
                }
            }
        }

        public async Task<bool> EliminarAutoAsync(int idAuto)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string queryVerificarAuto = "SELECT COUNT(*) FROM Auto WHERE ID_auto = @ID_auto";
                    using (SqlCommand commandVerificarAuto = new SqlCommand(queryVerificarAuto, connection))
                    {
                        commandVerificarAuto.Parameters.AddWithValue("@ID_auto", idAuto);
                        int autoExiste = (int)await commandVerificarAuto.ExecuteScalarAsync();
                        if (autoExiste == 0) return false;
                    }

                    string queryEliminarComprobantes = "DELETE FROM Comprobante WHERE ID_auto = @ID_auto";
                    using (SqlCommand commandEliminarComprobantes = new SqlCommand(queryEliminarComprobantes, connection))
                    {
                        commandEliminarComprobantes.Parameters.AddWithValue("@ID_auto", idAuto);
                        await commandEliminarComprobantes.ExecuteNonQueryAsync();
                    }

                    string queryEliminarAuto = "DELETE FROM Auto WHERE ID_auto = @ID_auto";
                    using (SqlCommand commandEliminarAuto = new SqlCommand(queryEliminarAuto, connection))
                    {
                        commandEliminarAuto.Parameters.AddWithValue("@ID_auto", idAuto);
                        return await commandEliminarAuto.ExecuteNonQueryAsync() > 0;
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al eliminar el auto: {ex.Message}", ex);
                    return false;
                }
            }
        }

        public async Task<List<Auto>> BuscarAutoAsync(string criterio)
        {
            List<Auto> autos = new List<Auto>();
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM Auto WHERE Estado = 'disponible' AND " +
                                   "(CAST(ID_auto AS NVARCHAR) LIKE @Criterio OR Marca LIKE @Criterio OR Modelo LIKE @Criterio " +
                                   "OR Color LIKE @Criterio OR Patente LIKE @Criterio OR CAST(Anio AS NVARCHAR) LIKE @Criterio)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Criterio", $"{criterio}%");
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync()) autos.Add(MapAuto(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al buscar el auto: {ex.Message}", ex);
                }
            }
            return autos;
        }

        public async Task<bool> VenderAutoAsync(int idAuto, int idEmpleado, string observaciones)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string queryAuto = "UPDATE Auto SET Estado = 'vendido' WHERE ID_auto = @ID_auto";
                        using (SqlCommand commandAuto = new SqlCommand(queryAuto, connection, transaction))
                        {
                            commandAuto.Parameters.AddWithValue("@ID_auto", idAuto);
                            if (await commandAuto.ExecuteNonQueryAsync() == 0) throw new Exception("No se pudo actualizar el estado del auto.");
                        }

                        string queryComprobante = "INSERT INTO Comprobante (Tipo, FechaHora, ID_auto, ID_empleado, Estado, Observaciones, Precio) " +
                                                   "VALUES ('venta', GETDATE(), @ID_auto, @ID_empleado, 'vendido', @Observaciones, " +
                                                   "(SELECT Precio FROM Auto WHERE ID_auto = @ID_auto))";
                        using (SqlCommand commandComprobante = new SqlCommand(queryComprobante, connection, transaction))
                        {
                            commandComprobante.Parameters.AddWithValue("@ID_auto", idAuto);
                            commandComprobante.Parameters.AddWithValue("@ID_empleado", idEmpleado);
                            commandComprobante.Parameters.AddWithValue("@Observaciones", observaciones);

                            if (await commandComprobante.ExecuteNonQueryAsync() == 0) throw new Exception("No se pudo generar el comprobante.");
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Logger.LogError($"Error al vender el auto: {ex.Message}", ex);
                        return false;
                    }
                }
            }
        }

        public async Task<List<Auto>> ObtenerTodosLosAutosAsync()
        {
            List<Auto> autos = new List<Auto>();
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM Auto";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync()) autos.Add(MapAuto(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al obtener los autos: {ex.Message}", ex);
                }
            }
            return autos;
        }

        public async Task<List<Auto>> BuscarAutosPorEstadoAsync(string estado)
        {
            List<Auto> autos = new List<Auto>();
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM Auto WHERE Estado = @Estado";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Estado", estado);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync()) autos.Add(MapAuto(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al buscar autos por estado: {ex.Message}", ex);
                }
            }
            return autos;
        }

        private Auto MapAuto(SqlDataReader reader)
        {
            return new Auto
            {
                ID_auto = reader.GetInt32(0),
                Marca = reader.GetString(1),
                Modelo = reader.GetString(2),
                Color = reader.GetString(3),
                Patente = reader.GetString(4),
                Anio = reader.GetInt32(5),
                Estado = reader.GetString(6),
                ID_empleado = reader.GetInt32(7),
                Precio = reader.GetDecimal(8)
            };
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;
using ConcesionarioWEBFORM1111.Model.Utilities;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public class ServicioDAO : IServicioDAO
    {
        private readonly DataBaseConnection dbConnection;

        public ServicioDAO(DataBaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<bool> AgregarServicioAsync(Servicio servicio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO Servicios (Descripcion, Fecha, Estado, ID_empleado) " +
                                   "VALUES (@Descripcion, @Fecha, @Estado, @ID_empleado)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Descripcion", servicio.Descripcion);
                        cmd.Parameters.AddWithValue("@Fecha", servicio.Fecha);
                        cmd.Parameters.AddWithValue("@Estado", "En proceso");
                        cmd.Parameters.AddWithValue("@ID_empleado", servicio.ID_empleado);
                        await cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error al agregar servicio: " + ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> MarcarComoRealizadoAsync(int idServicio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE Servicios SET Estado = 'Realizado' WHERE ID_servicio = @ID_servicio";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_servicio", idServicio);
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error al marcar como realizado: " + ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> EliminarServicioAsync(int idServicio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    await conn.OpenAsync();
                    string query = "DELETE FROM Servicios WHERE ID_servicio = @ID_servicio";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_servicio", idServicio);
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error al eliminar servicio: " + ex.Message, ex);
                return false;
            }
        }

        public async Task<List<Servicio>> ObtenerTodosLosServiciosAsync()
        {
            List<Servicio> lista = new List<Servicio>();
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT * FROM Servicios";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                lista.Add(new Servicio
                                {
                                    ID_servicio = reader.GetInt32(0),
                                    Descripcion = reader.GetString(1),
                                    Fecha = reader.GetDateTime(2),
                                    Estado = reader.GetString(3),
                                    ID_empleado = reader.GetInt32(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error al obtener servicios: " + ex.Message, ex);
            }
            return lista;
        }
    }
}


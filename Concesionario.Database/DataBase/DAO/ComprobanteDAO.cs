using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConcesionarioWEBFORM1111.Model;
using ConcesionarioWEBFORM1111.Model.Utilities;

namespace ConcesionarioWEBFORM1111.DataBase.DAO
{
    public class ComprobanteDAO : IComprobanteDAO
    {
        private readonly DataBaseConnection dbConnection;

        public ComprobanteDAO(DataBaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<List<Comprobante>> ObtenerTodosLosComprobantesAsync()
        {
            List<Comprobante> comprobantes = new List<Comprobante>();
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM Comprobante";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                comprobantes.Add(MapearComprobante(reader));
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Logger.LogError($"Error al obtener todos los comprobantes: {ex.Message}", ex);
                }
            }
            return comprobantes;
        }

        private Comprobante MapearComprobante(SqlDataReader reader)
        {
            return new Comprobante
            {
                ID_comprobante = reader.GetInt32(0),
                Tipo = reader.GetString(1),
                FechaHora = reader.GetDateTime(2),
                ID_auto = reader.GetInt32(3),
                ID_empleado = reader.GetInt32(4),
                Estado = reader.IsDBNull(5) ? null : reader.GetString(5),
                Observaciones = reader.IsDBNull(6) ? null : reader.GetString(6),
                Precio = reader.GetDecimal(7)
            };
        }
    }
}


using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class AreaData
    {
        private readonly string conexion;

        public AreaData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public async Task<List<Area>> ListaArea()
        {
            List<Area> lista = new List<Area>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("sp_Listar_Area", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Area
                            {
                                id_area = reader.GetByte(reader.GetOrdinal("ID_Area")),
                                nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                                id_empresa = reader.GetByte(reader.GetOrdinal("ID_Empresa")),
                                estado = reader.IsDBNull(reader.GetOrdinal("Estado"))
                                         ? null
                                         : reader.GetBoolean(reader.GetOrdinal("Estado"))
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<Area> ListarAreaPorID(int id)
        {
            Area? area = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Area_Por_ID", conex);
                cmd.Parameters.AddWithValue("@ID_Area", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        area = new Area
                        {
                            id_area = reader.GetByte(reader.GetOrdinal("ID_Area")),
                            nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                            id_empresa = reader.GetByte(reader.GetOrdinal("ID_Empresa")),
                            estado = reader.IsDBNull(reader.GetOrdinal("Estado"))
                                         ? null
                                         : reader.GetBoolean(reader.GetOrdinal("Estado"))
                        };
                    }
                }
            }

            return area!;
        }

        public async Task<Area?> ListarAreaPorNombre(string nombre)
        {
            Area? area = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Area_Por_Nombre", conex);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        area = new Area
                        {
                            id_area = reader.GetByte(reader.GetOrdinal("ID_Area")),
                            nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                            id_empresa = reader.GetByte(reader.GetOrdinal("ID_Empresa")),
                            estado = reader.IsDBNull(reader.GetOrdinal("Estado"))
                                         ? null
                                         : reader.GetBoolean(reader.GetOrdinal("Estado"))
                        };
                    }
                }
            }

            return area;
        }

        public async Task<bool> AgregarArea(Area area)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Insertar_Area", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", area.nombre);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)area.descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ID_Empresa", area.id_empresa);
                cmd.Parameters.AddWithValue("@Estado", (object?)area.estado ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ActualizarArea(Area area)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Actualizar_Area", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Area", area.id_area);
                cmd.Parameters.AddWithValue("@Nombre", area.nombre);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)area.descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object?)area.estado ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> EliminarArea(byte idArea)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_Area", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Empresa", idArea);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }
    }
}

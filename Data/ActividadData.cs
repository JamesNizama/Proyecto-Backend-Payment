using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class ActividadData
    {
        private readonly string conexion;

        public ActividadData(IConfiguration configuration)  
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public async Task<List<Actividad>> ListaActividad()
        {
            List<Actividad> lista = new List<Actividad>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("sp_Listar_Actividad", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Actividad
                            {
                                id_actividad = reader.GetInt32(reader.GetOrdinal("ID_Actividad")),
                                id_usuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                                titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                                fecha_inicio = reader.IsDBNull(reader.GetOrdinal("Fecha_Inicio"))
                                       ? (DateTime?)null
                                       : reader.GetDateTime(reader.GetOrdinal("Fecha_Inicio")),
                                fecha_fin = reader.IsDBNull(reader.GetOrdinal("Fecha_Fin"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("Fecha_Fin")),
                                estado = reader.GetString(reader.GetOrdinal("Estado")),
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<Actividad> ListarActividadPorID(int id)
        {
            Actividad? actividad = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Actividad_Por_ID", conex);
                cmd.Parameters.AddWithValue("@ID_Actividad", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        actividad = new Actividad
                        {
                            id_actividad = reader.GetInt32(reader.GetOrdinal("ID_Actividad")),
                            id_usuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                            titulo = reader["Titulo"].ToString()!,
                            descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                            fecha_inicio = reader.IsDBNull(reader.GetOrdinal("Fecha_Inicio"))
                                       ? (DateTime?)null
                                       : reader.GetDateTime(reader.GetOrdinal("Fecha_Inicio")),
                            fecha_fin = reader.IsDBNull(reader.GetOrdinal("Fecha_Fin"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("Fecha_Fin")),
                            estado = reader.GetString(reader.GetOrdinal("Estado"))
                        };
                    }
                }
            }

            return actividad!;
        }

        public async Task<Actividad?> ListarActividadPorTitulo(string titulo)
        {
            Actividad? actividad = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Actividad_Por_Titulo", conex);
                cmd.Parameters.AddWithValue("@Titulo", titulo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        actividad = new Actividad
                        {
                            id_actividad = reader.GetInt32(reader.GetOrdinal("ID_Actividad")),
                            id_usuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                            titulo = reader["Titulo"].ToString()!,
                            descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            fecha_inicio = reader.IsDBNull(reader.GetOrdinal("Fecha_Inicio"))
                                       ? (DateTime?)null
                                       : reader.GetDateTime(reader.GetOrdinal("Fecha_Inicio")),
                            fecha_fin = reader.IsDBNull(reader.GetOrdinal("Fecha_Fin"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("Fecha_Fin")),
                            estado = reader.GetString(reader.GetOrdinal("Estado"))
                        };
                    }
                }
            }

            return actividad;
        }

        public async Task<bool> AgregarActividad(Actividad actividad)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Insertar_Actividad", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Usuario", actividad.id_usuario);
                cmd.Parameters.AddWithValue("@Titulo", actividad.titulo);
                cmd.Parameters.AddWithValue("@Fecha_Inicio", (object?)actividad.fecha_inicio ?? DBNull.Value); 
                cmd.Parameters.AddWithValue("@Fecha_Fin", (object?)actividad.fecha_fin ?? DBNull.Value); 
                cmd.Parameters.AddWithValue("@Descripcion", (object?)actividad.descripcion ?? DBNull.Value); 
                cmd.Parameters.AddWithValue("@Estado", (object?)actividad.estado ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ActualizarActividad(Actividad actividad)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Actualizar_Actividad", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Actividad", actividad.id_actividad);
                cmd.Parameters.AddWithValue("@Titulo", actividad.titulo);
                cmd.Parameters.AddWithValue("@Fecha_Inicio", (object?)actividad.fecha_inicio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fecha_Fin", (object?)actividad.fecha_fin ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)actividad.descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object?)actividad.estado ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> EliminarActividad(byte idActividad)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_Actividad", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Actividad", idActividad);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }



    }
}


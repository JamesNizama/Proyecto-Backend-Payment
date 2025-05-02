using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class ProyectoData
    {
        private readonly string conexion;

        public ProyectoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public async Task<List<Proyecto>> ListaProyecto()
        {
            List<Proyecto> lista = new List<Proyecto>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("sp_Listar_Proyecto", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Proyecto
                            {
                                id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                id_usuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                                titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? null : reader.GetString(reader.GetOrdinal("descripcion")),
                                fecha_limite = reader.GetDateTime(reader.GetOrdinal("fecha_limite")),
                                progreso = reader.GetInt32(reader.GetOrdinal("progreso")),
                                fecha_creacion = reader.GetDateTime(reader.GetOrdinal("fecha_creacion")),
                                fecha_actualizacion = reader.GetDateTime(reader.GetOrdinal("fecha_actualizacion")),
                                estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? "No Iniciado" : reader.GetString(reader.GetOrdinal("estado")),
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<int> AgregarProyecto(Proyecto proyecto)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                SqlCommand cmd = new SqlCommand("sp_Insertar_Proyecto", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@titulo", proyecto.titulo);
                cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(proyecto.descripcion) ? DBNull.Value : (object)proyecto.descripcion);
                cmd.Parameters.AddWithValue("@fecha_limite", proyecto.fecha_limite == default ? DBNull.Value : (object)proyecto.fecha_limite);
                cmd.Parameters.AddWithValue("@id_usuario", proyecto.id_usuario);
                cmd.Parameters.AddWithValue("@progreso", proyecto.progreso);
                cmd.Parameters.AddWithValue("@estado", proyecto.estado ?? "No Iniciado");

                object resultado = await cmd.ExecuteScalarAsync();

                // Retornar el ID del proyecto recién insertado o 0 si no se generó correctamente
                return resultado != null ? Convert.ToInt32(resultado) : 0;
            }
        }



        public async Task<bool> ActualizarProyecto(Proyecto proyecto)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("sp_Actualizar_Proyecto", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_proyecto", proyecto.id_proyecto);
                    cmd.Parameters.AddWithValue("@titulo", proyecto.titulo);
                    cmd.Parameters.AddWithValue("@descripcion", (object?)proyecto.descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_limite", proyecto.fecha_limite);
                    cmd.Parameters.AddWithValue("@id_usuario", proyecto.id_usuario);
                    cmd.Parameters.AddWithValue("@progreso", proyecto.progreso);
                    cmd.Parameters.AddWithValue("@estado", proyecto.estado);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }






    }
}

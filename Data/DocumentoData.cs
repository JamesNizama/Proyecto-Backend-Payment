using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class DocumentoData
    {
        private readonly string conexion;

        public DocumentoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public async Task<List<Documento>> ListaDocumento()
        {
            List<Documento> lista = new List<Documento>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("sp_Listar_Documento", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Documento
                            {
                                id_documento = reader.GetInt32(reader.GetOrdinal("ID_Documento")),
                                titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                tipo = reader.GetString(reader.GetOrdinal("Tipo")),
                                descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                                fecha_creacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Creacion"))
                                       ? (DateTime?)null
                                       : reader.GetDateTime(reader.GetOrdinal("Fecha_Creacion")),
                                fecha_ultima_modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Ultima_Modificacion"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("Fecha_Ultima_Modificacion")),
                                autor = reader.GetInt32(reader.GetOrdinal("Autor"))
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<Documento> ListarDocumentoPorID(int id)
        {
            Documento? documento = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Documento_Por_ID", conex);
                cmd.Parameters.AddWithValue("@ID_Documento", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        documento = new Documento
                        {
                            id_documento = reader.GetInt32(reader.GetOrdinal("ID_Documento")),
                            titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            tipo = reader.GetString(reader.GetOrdinal("Tipo")),
                            descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                            fecha_creacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Creacion"))
                                       ? (DateTime?)null
                                       : reader.GetDateTime(reader.GetOrdinal("Fecha_Creacion")),
                            fecha_ultima_modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Ultima_Modificacion"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("Fecha_Ultima_Modificacion")),
                            autor = reader.GetInt32(reader.GetOrdinal("Autor"))
                        };
                    }
                }
            }

            return documento!;
        }

        public async Task<Documento?> ListarDocumentoPorTitulo(string titulo)
        {
            Documento? documento= null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Documento_Por_Titulo", conex);
                cmd.Parameters.AddWithValue("@Titulo", titulo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        documento = new Documento
                        {
                            id_documento = reader.GetInt32(reader.GetOrdinal("ID_Documento")),
                            titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            tipo = reader.GetString(reader.GetOrdinal("Tipo")),
                            descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                            fecha_creacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Creacion"))
                                       ? (DateTime?)null
                                       : reader.GetDateTime(reader.GetOrdinal("Fecha_Creacion")),
                            fecha_ultima_modificacion = reader.IsDBNull(reader.GetOrdinal("Fecha_Ultima_Modificacion"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("Fecha_Ultima_Modificacion")),
                            autor = reader.GetInt32(reader.GetOrdinal("Autor"))
                        };
                    }
                }
            }

            return documento;
        }

        public async Task<bool> AgregarDocumento(Documento documento)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Insertar_Documento0", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Titulo", documento.titulo);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)documento.descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tipo", documento.tipo);
                cmd.Parameters.AddWithValue("@Autor", (object?)documento.autor ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ActualizarDocumento(Documento documento)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Actualizar_Documento", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Documento", documento.id_documento);
                cmd.Parameters.AddWithValue("@Titulo", documento.titulo);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)documento.descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Tipo", documento.tipo);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> EliminarDocumento(byte idDocumento)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_Documento", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Documento", idDocumento);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class UsuarioData
    {
        private readonly string conexion;
        public UsuarioData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public async Task<List<Usuarios>> ListaUsuario()
        {
            List<Usuarios> lista = new List<Usuarios>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();    

                using (var cmd = new SqlCommand("sp_Listar_Usuario", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Usuarios
                            {
                                id_usuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                                nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                                direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                correo_electronico = reader.GetString(reader.GetOrdinal("Correo_Electronico")),
                                telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                fecha_contrato = reader.GetDateTime(reader.GetOrdinal("Fecha_Contratacion")),
                                cargo = reader.GetString(reader.GetOrdinal("Cargo")),
                                id_area = reader.GetByte(reader.GetOrdinal("ID_Area")),
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

        public async Task<Usuarios> ListarUsuarioPorID(int id)
        {
            Usuarios? usuarios = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Usuario_Por_ID", conex);
                cmd.Parameters.AddWithValue("@ID_Usuario", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        usuarios = new Usuarios
                        {
                            id_usuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                            nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                            direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                            correo_electronico = reader.GetString(reader.GetOrdinal("Correo_Electronico")),
                            telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                            fecha_contrato = reader.GetDateTime(reader.GetOrdinal("Fecha_Contratacion")),
                            cargo = reader.GetString(reader.GetOrdinal("Cargo")),
                            id_area = reader.GetByte(reader.GetOrdinal("ID_Area")),
                            estado = reader.IsDBNull(reader.GetOrdinal("Estado"))
                                         ? null
                                         : reader.GetBoolean(reader.GetOrdinal("Estado"))
                        };
                    }
                }
            }

            return usuarios!;
        }

        public async Task<Usuarios?> ListarUsuarioPorNombre(string nombre)
        {
            Usuarios? usuarios = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Usuario_Por_Nombre", conex);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        usuarios = new Usuarios
                        {
                            id_usuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                            nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                            direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                            correo_electronico = reader.GetString(reader.GetOrdinal("Correo_Electronico")),
                            telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                            fecha_contrato = reader.GetDateTime(reader.GetOrdinal("Fecha_Contratacion")),
                            cargo = reader.GetString(reader.GetOrdinal("Cargo")),
                            id_area = reader.GetByte(reader.GetOrdinal("ID_Area")),
                            estado = reader.IsDBNull(reader.GetOrdinal("Estado"))
                                         ? null
                                         : reader.GetBoolean(reader.GetOrdinal("Estado"))
                        };
                    }
                }
            }

            return usuarios;
        }

        public async Task<bool> AgregarUsuario(Usuarios usuarios)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Insertar_Usuario", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", usuarios.nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuarios.apellido);
                cmd.Parameters.AddWithValue("@Direccion", usuarios.direccion);
                cmd.Parameters.AddWithValue("@Correo_Electronico", usuarios.correo_electronico);
                cmd.Parameters.AddWithValue("@Telefono", usuarios.telefono);
                cmd.Parameters.AddWithValue("@Fecha_Contratacion", (object?)usuarios.fecha_contrato ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cargo", usuarios.cargo);
                cmd.Parameters.AddWithValue("@ID_Area", usuarios.id_area);
                cmd.Parameters.AddWithValue("@Estado", (object?)usuarios.estado ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ActualizarUsuario(Usuarios usuarios)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Actualizar_Usuario", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Usuario", usuarios.id_usuario);
                cmd.Parameters.AddWithValue("@Nombre", usuarios.nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuarios.apellido);
                cmd.Parameters.AddWithValue("@Direccion", usuarios.direccion);
                cmd.Parameters.AddWithValue("@Correo_Electronico", usuarios.correo_electronico);
                cmd.Parameters.AddWithValue("@Telefono", usuarios.telefono);
                cmd.Parameters.AddWithValue("@Fecha_Contratacion", (object?)usuarios.fecha_contrato ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cargo", usuarios.cargo);
                cmd.Parameters.AddWithValue("@ID_Area", usuarios.id_area);
                cmd.Parameters.AddWithValue("@Estado", (object?)usuarios.estado ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> EliminarUsuarios(byte idUsuario)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_Usuario", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Usuario", idUsuario);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }
    }
}

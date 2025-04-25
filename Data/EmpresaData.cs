using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class EmpresaData
    {
        private readonly string conexion;

        public EmpresaData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public async Task<List<Empresa>> ListaEmpresa()
        {
            List<Empresa> lista = new List<Empresa>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("sp_Listar_Empresa", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Empresa
                            {
                                id_empresa = reader.GetByte(reader.GetOrdinal("ID_Empresa")),
                                nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                ruc = reader.GetString(reader.GetOrdinal("RUC")),
                                descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
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

        public async Task<Empresa> ListarEmpresaPorID(int id)
        {
            Empresa? empresa = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Empresa_Por_ID", conex);
                cmd.Parameters.AddWithValue("@ID_Empresa", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        empresa = new Empresa
                        {
                            id_empresa = Convert.ToByte(reader["ID_Empresa"]),
                            nombre = reader["Nombre"].ToString()!,
                            ruc = reader["RUC"].ToString()!,
                            descripcion = reader["Descripcion"]?.ToString(),
                            estado = reader["Estado"] != DBNull.Value ? Convert.ToBoolean(reader["Estado"]) : (bool?)null
                        };
                    }
                }
            }

            return empresa!;
        }

        public async Task<Empresa?> ListarEmpresaPorNombre(string nombre)
        {
            Empresa? empresa = null;

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("Listar_Empresa_Por_Nombre", conex);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        empresa = new Empresa
                        {
                            id_empresa = Convert.ToByte(reader["ID_Empresa"]),
                            nombre = reader["Nombre"].ToString()!,
                            ruc = reader["RUC"].ToString()!,
                            descripcion = reader["Descripcion"]?.ToString(),
                            estado = reader["Estado"] != DBNull.Value ? Convert.ToBoolean(reader["Estado"]) : (bool?)null
                        };
                    }
                }
            }

            return empresa;
        }

        public async Task<bool> AgregarEmpresa(Empresa empresa)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Insertar_Empresa", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", empresa.nombre);
                cmd.Parameters.AddWithValue("@RUC", empresa.ruc);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)empresa.descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object?)empresa.estado ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ActualizarEmpresa(Empresa empresa)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Actualizar_Empresa", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Empresa", empresa.id_empresa);
                cmd.Parameters.AddWithValue("@Nombre", empresa.nombre);
                cmd.Parameters.AddWithValue("@RUC", empresa.ruc);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)empresa.descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", (object?)empresa.estado ?? DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> EliminarEmpresa(byte idEmpresa)
        {
            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Eliminar_Empresa", conex);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Empresa", idEmpresa);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }



    }
}

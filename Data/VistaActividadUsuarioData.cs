using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class VistaActividadUsuarioData
    {
        private readonly string conexion;

        public VistaActividadUsuarioData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public async Task<List<VistaActividadesUsuario>> ListaVistaActividadUsuarioData()
        {
            List<VistaActividadesUsuario> lista = new List<VistaActividadesUsuario>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("SELECT * FROM Vista_Actividades_Por_Usuario", conex))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new VistaActividadesUsuario
                            {
                                id_usuario = reader.GetInt32(reader.GetOrdinal("ID_Usuario")),
                                nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                                id_actividad = reader.GetInt32(reader.GetOrdinal("ID_Actividad")),
                                titulo = reader.GetString(reader.GetOrdinal("titulo")),
                                descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                                fecha_inicio = reader.GetDateTime(reader.GetOrdinal("Fecha_Inicio")),
                                fecha_fin = reader.GetDateTime(reader.GetOrdinal("Fecha_Fin")),
                                estado = reader.GetString(reader.GetOrdinal("Estado"))
                            });
                        }
                    }
                }
            }

            return lista;
        }

    }
}

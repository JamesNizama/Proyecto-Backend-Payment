using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class VistaActividadPendienteData
    {
        private readonly string conexion;

        public VistaActividadPendienteData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public async Task<List<VistaActividadesPendientes>> ListaVistaActividadPendienteData()
        {
            List<VistaActividadesPendientes> lista = new List<VistaActividadesPendientes>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("SELECT * FROM Vista_Actividades_Pendientes", conex))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new VistaActividadesPendientes
                            {
                                id_actividad = reader.GetInt32(reader.GetOrdinal("ID_Actividad")),
                                titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                              ? null
                                              : reader.GetString(reader.GetOrdinal("Descripcion")),
                                fecha_inicio = reader.GetDateTime(reader.GetOrdinal("Fecha_Inicio")),
                                fecha_fin = reader.GetDateTime(reader.GetOrdinal("Fecha_Fin")),
                                estado = reader.GetString(reader.GetOrdinal("Estado")),
                                nombre_usuario = reader.GetString(reader.GetOrdinal("Nombre_Usuario")),
                                apellido_usuario = reader.GetString(reader.GetOrdinal("Apellido_Usuario"))
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}

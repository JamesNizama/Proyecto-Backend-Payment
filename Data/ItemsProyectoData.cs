using Microsoft.Data.SqlClient;
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class ItemsProyectoData
    {
        private readonly string conexion;

        // Constructor que obtiene la cadena de conexión desde IConfiguration
        public ItemsProyectoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        // Método para obtener todos los ítems del proyecto
        public async Task<List<ItemProyecto>> ListaItemsProyecto()
        {
            List<ItemProyecto> listaItems = new List<ItemProyecto>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();

                using (var cmd = new SqlCommand("sp_Listar_items_proyecto", conex))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listaItems.Add(new ItemProyecto
                            {
                                id_items = reader.GetInt32(reader.GetOrdinal("ID_ITEMS")),
                                id_proyecto = reader.GetInt32(reader.GetOrdinal("ID_PROYECTO")),
                                descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? null : reader.GetString(reader.GetOrdinal("descripcion")),
                                estado = reader.GetString(reader.GetOrdinal("estado")),
                                fecha_creacion = reader.GetDateTime(reader.GetOrdinal("fecha_creacion"))
                            });
                        }
                    }
                }
            }

            return listaItems;
        }

        public async Task<List<ItemProyecto>> ListaItemsProyecto(int id)
        {
            var items = new List<ItemProyecto>();

            using (var conex = new SqlConnection(conexion))
            {
                await conex.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_Listar_items_proyecto_por_id", conex);
                cmd.Parameters.AddWithValue("@ID_PROYECTO", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new ItemProyecto
                        {
                            id_items = reader.GetInt32(reader.GetOrdinal("ID_ITEMS")),
                            id_proyecto = reader.GetInt32(reader.GetOrdinal("ID_PROYECTO")),
                            descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? null : reader.GetString(reader.GetOrdinal("descripcion")),
                            estado = reader.GetString(reader.GetOrdinal("estado")),
                            fecha_creacion = reader.GetDateTime(reader.GetOrdinal("fecha_creacion"))
                        });
                    }
                }
            }

            return items;
        }


        // Método para agregar un ítem de proyecto
        public async Task<int> AgregarItemProyecto(ItemProyecto itemProyecto)
        {
            try
            {
                using (var conex = new SqlConnection(conexion))
                {
                    await conex.OpenAsync();
                    SqlCommand cmd = new SqlCommand("sp_Insertar_ItemProyecto", conex);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Usar DBNull para valores nulos en la base de datos
                    cmd.Parameters.AddWithValue("@id_proyecto", itemProyecto.id_proyecto);
                    cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(itemProyecto.descripcion) ? DBNull.Value : (object)itemProyecto.descripcion);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(itemProyecto.estado) ? DBNull.Value : (object)itemProyecto.estado);

                    // Ejecutar el comando y obtener el ID del ítem recién insertado
                    object resultado = await cmd.ExecuteScalarAsync();

                    // Retornar el ID del ítem recién insertado 
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores SQL, podría incluir un log de errores o más detalles
                throw new Exception("Error al insertar el ítem del proyecto.", ex);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                throw new Exception("Error general al insertar el ítem del proyecto.", ex);
            }
        }
    }
}

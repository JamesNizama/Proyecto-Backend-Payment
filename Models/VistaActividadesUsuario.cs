namespace MiWebAPI.Models
{
    public class VistaActividadesUsuario
    {
        public int id_usuario { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public int id_actividad { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public string? estado { get; set; }
    }
}

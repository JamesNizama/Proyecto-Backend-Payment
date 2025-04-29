namespace MiWebAPI.Models
{
    public class Actividad
    {
        public int id_actividad { get; set; }
        public int id_usuario { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_fin { get; set; }
        public string? estado { get; set; }

    }
}

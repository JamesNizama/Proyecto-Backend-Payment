namespace MiWebAPI.Models
{
    public class Documento
    {
        public int id_documento { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string tipo { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public DateTime? fecha_ultima_modificacion { get; set; }
        public int autor { get; set; }

    }
}

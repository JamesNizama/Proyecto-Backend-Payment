namespace MiWebAPI.Models
{
    public class Area
    {
        public byte id_area { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public byte id_empresa { get; set; }
        public bool? estado { get; set; }

    }
}

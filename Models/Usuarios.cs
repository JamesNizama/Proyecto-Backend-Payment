namespace MiWebAPI.Models
{
    public class Usuarios
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string correo_electronico { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public DateTime? fecha_contrato { get; set; }
        public string cargo { get; set; } = string.Empty;
        public byte id_area { get; set; }
        public bool? estado { get; set; }
        public string user { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}

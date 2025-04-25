namespace MiWebAPI.Models
{
    public class Empresa
    {
        public byte id_empresa { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string ruc { get; set; } = string.Empty;
        public string? descripcion { get; set; }
        public bool? estado { get; set; }
    }
}

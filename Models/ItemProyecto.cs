using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.Models
{
    public class ItemProyecto
    {
        public int id_items { get; set; }

        public int id_proyecto { get; set; }

        public string? descripcion { get; set; }

        public string estado { get; set; } = "No Iniciado"; 

        public DateTime fecha_creacion { get; set; } = DateTime.Now;

    }
}

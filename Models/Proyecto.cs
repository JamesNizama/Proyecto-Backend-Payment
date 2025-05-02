using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiWebAPI.Models
{
    public class Proyecto
    {
        public int id_proyecto { get; set; }
        public int id_usuario { get; set; }

        public string? titulo { get; set; }

        public string? descripcion { get; set; }

        public DateTime fecha_limite { get; set; }

        public string estado { get; set; } = "No Iniciado"; 

        public int progreso { get; set; } = 0; 

        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        public DateTime fecha_actualizacion { get; set; } = DateTime.Now;

        
    }
}

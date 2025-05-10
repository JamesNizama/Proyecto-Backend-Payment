namespace MiWebAPI.Models
{
    public class Horarios_Trabajo
    {
        public int ID_Horario { get; set; }
        public int ID_Usuario { get; set; }
        public string? Estado { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora_Inicio { get; set; }
        public TimeSpan Hora_Fin { get; set; }
        public decimal Horas_Trabajadas { get; set; }
        public TimeSpan Pausas { get; set; }
        public decimal Total { get; set; }
    }
}

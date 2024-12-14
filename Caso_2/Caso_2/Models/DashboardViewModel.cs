namespace Caso_2.Models
{
    public class DashboardViewModel
    {
        public int TotalUsuarios { get; set; }
        public int TotalEventos { get; set; }
        public int TotalInscritosMes { get; set; }
        public int TotalCategorias { get; set; }
        public string MesAnioActual { get; set; }
        public List<string> TopEventos { get; set; } = new List<string>();
    }

}
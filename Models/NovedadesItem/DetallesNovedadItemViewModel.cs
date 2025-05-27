namespace InventarioLPS.Models.NovedadesItem
{
    public class DetallesNovedadItemViewModel
    {
        // CAMPOS PARA DETALLES.
        public int Id { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public string Novedad { get; set; }
    }
}

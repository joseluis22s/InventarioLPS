namespace InventarioLPS.Models.Inventario
{
    public class RegistroInventarioViewModel
    {
        public string FormaRegistro { get; set; }

        public string NumeroDocumento { get; set; }

        public DateTime FechaCompra { get; set; }

        public List<ItemInventarioClonViewModel> Items { get; set; } = new();
    }
}

namespace InventarioLPS.Models.Inventario
{
    public class RegistroInventarioViewModel
    {
        public string FormaRegistro { get; set; }

        public string NumeroDocumento { get; set; }

        public DateTime FechaCompra { get; set; }


        public List<ItemCantidadViewModel> RegistroPorCantidad { get; set; } = new();
        public List<ItemDuplicadoViewModel> RegistroDuplicado { get; set; } = new();
    }
}

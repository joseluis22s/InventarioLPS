using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace InventarioLPS.Models.Inventario
{
    public class RegistroInventarioViewModel
    {
        // CAMPOS PARA EL DOCUMENTO

        [Required(ErrorMessage = "*Campo requerido.")]
        public string FormaRegistro { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime FechaCompra { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "*Campo requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal MontoTotal { get; set; }

        [ValidateEnumeratedItems]
        public List<ItemInventarioViewModel> Items { get; set; } = new();
    }
}

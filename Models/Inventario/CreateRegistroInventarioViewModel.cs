using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace InventarioLPS.Models.Inventario
{
    public class CreateRegistroInventarioViewModel
    {
        // CAMPOS DEL DOCUMENTO.
        [Required(ErrorMessage = "*Campo requerido.")]
        public int FormaRegistroId { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime FechaCompra { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "*Campo requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal MontoTotal { get; set; }


        // ITEMS.
        [ValidateEnumeratedItems]
        public List<ItemInventarioViewModel> Items { get; set; } = new();


        // LISTAS PARA LOS SELECTS.
        public IEnumerable<SelectListItem>? FormasRegistroOptions { get; set; }
        public IEnumerable<SelectListItem>? ProveedoresOptions { get; set; }
        public IEnumerable<SelectListItem>? ProductosOptions { get; set; }
        public IEnumerable<SelectListItem>? UbicacionesOptions { get; set; }
        public IEnumerable<SelectListItem>? EstatusOptions { get; set; }
        public IEnumerable<SelectListItem>? ClasificacionesOptions { get; set; }

        // JSON CON INFO DEL PRODUCTO PARA EL SCRIPT.
        public string? ProductInfoJson { get; set; }

    }
}

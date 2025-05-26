using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [DataType(DataType.Date)]
        public DateTime FechaCompra { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "*Campo requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal MontoTotal { get; set; }


        // ITEMS.
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

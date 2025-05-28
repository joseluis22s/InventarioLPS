using System.ComponentModel.DataAnnotations;

namespace InventarioLPS.Models.NovedadesItem
{
    public class NovedadesViewModel
    {
        public int IdItemInventario { get; set; }

        // CAMPOS PARA `_ResumenNovedadesPartial.cshtml`
        public string? CodigoItem { get; set; }

        public string? NombreProducto { get; set; }

        public string? DescripcionEspecifica { get; set; }

        public string? EspecificacionesTecnicas { get; set; }

        public string? FormaRegistro { get; set; }

        public string? NumeroDocumento { get; set; }

        public DateTime? FechaCompra { get; set; }

        public List<DetallesNovedadItemViewModel>? Detalles { get; set; }

        // CAMPOS EXTRA PARA Create.cshtml
        [Required(ErrorMessage = "*Campo requerido.")]
        [DataType(DataType.Date)]
        public DateTime FechaNuevaNovedad { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "*Campo requerido.")]
        public string NuevaNovedad { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace InventarioLPS.Models.Inventario
{
    public class ItemInventarioViewModel
    {
        [Required(ErrorMessage = "*Campo requerido.")]
        public string CodigoItem { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0.")]
        public decimal ValorSinIva { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string Proveedor { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string Producto { get; set; }

        public string Departamento { get; set; }

        public string Categoria { get; set; }

        public string LineaServicio { get; set; }

        public string SubLineaServicio { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string DescripcionEspecifica { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string EspecificacionesTecnicas { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string NumeroParteFabricante { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string NumeroSerieLps { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string Estatus { get; set; }

        [Required(ErrorMessage = "*Campo requerido.")]
        public string Clasificacion { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventarioLPS.Models.Inventario
{
    public class InventarioCreateViewModel
    {
        // DATOS DEL DOCUEMENTO
        public RegistroInventarioViewModel Registro { get; set; } = new();

        // INFO PARA LOS SELECTS
        public IEnumerable<SelectListItem> FormasRegistro { get; set; }
        public IEnumerable<SelectListItem> Proveedores { get; set; }
        public IEnumerable<SelectListItem> Productos { get; set; }
        public IEnumerable<SelectListItem> Ubicaciones { get; set; }
        public IEnumerable<SelectListItem> Estatus { get; set; }
        public IEnumerable<SelectListItem> Clasificaciones { get; set; }

        // INFO ADICIONAL PARA PRODUCTO
        public string ProductInfoJson { get; set; }
    }

}

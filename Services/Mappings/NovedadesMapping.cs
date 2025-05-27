using InventarioLPS.Data.Entities;
using InventarioLPS.Models.NovedadesItem;

namespace InventarioLPS.Services.Mappings
{
    public class NovedadesMapping
    {
        public static NovedadesViewModel ToNovedadesItemViewModel(ItemInventario cabecera, List<NovedadItem> detalles)
        {
            return new NovedadesViewModel
            {
                IdItemInventario = cabecera.Id,
                CodigoItem = cabecera.CodigoItem,
                NombreProducto = cabecera.CodigoProductoNavigation.Nombre,
                DescripcionEspecifica = cabecera.DescripcionEspecifica,
                EspecificacionesTecnicas = cabecera.EspecificacionesTecnicas,
                FormaRegistro = cabecera.IdInformacionRegistroNavigation.IdFormaRegistroNavigation.Nombre,
                NumeroDocumento = cabecera.IdInformacionRegistroNavigation.NumeroDocumento,
                FechaCompra = cabecera.IdInformacionRegistroNavigation.FechaCompra,
                Detalles = ToDetallesNovedadItemViewModelList(detalles)
            };
        }

        public static DetallesNovedadItemViewModel NovedadItemToDetallesNovedadItemViewModel(NovedadItem novedadItem)
        {
            return new DetallesNovedadItemViewModel
            {
                Id = novedadItem.Id,
                Fecha = novedadItem.Fecha,
                Novedad = novedadItem.Novedad,
            };
        }

        public static List<DetallesNovedadItemViewModel> ToDetallesNovedadItemViewModelList(List<NovedadItem> novedadesItems) =>
            novedadesItems.Select(n => NovedadItemToDetallesNovedadItemViewModel(n)).ToList();

        public static NovedadItem ToNovedadItem(NovedadesViewModel model)
        {
            return new NovedadItem
            {
                Fecha = model.FechaNuevaNovedad,
                Novedad = model.NuevaNovedad,
                IdItemInventario = model.IdItemInventario
            };
        }
    }
}


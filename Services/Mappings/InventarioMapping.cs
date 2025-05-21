using InventarioLPS.Data.Entities;
using InventarioLPS.Models.Inventario;

namespace InventarioLPS.Services.Mappings
{
    public static class InventarioMapping
    {
        public static InventarioViewModel ToViewModel(ItemInventario itemInventario)
        {
            return new InventarioViewModel
            {
                Id = itemInventario.Id,
                CodigoItem = itemInventario.CodigoItem,
                FormaRegistro = itemInventario.IdInformacionRegistroNavigation.IdFormaRegistroNavigation.Nombre,
                NumeroDocumento = itemInventario.IdInformacionRegistroNavigation.NumeroDocumento,
                Departamento = itemInventario.Departamento,
                Categoria = itemInventario.Categoria,
                LineaServicio = itemInventario.LineaServicio,
                SubLineaServicio = itemInventario.SubLineaServicio,
                ValorSinIva = itemInventario.ValorSinIva,
                Proveedor = itemInventario.IdProveedorNavigation.RazonSocial,
                Producto = itemInventario.CodigoProductoNavigation.Nombre,
                DescripcionEspecifica = itemInventario.DescripcionEspecifica,
                EspecificacionesTecnicas = itemInventario.EspecificacionesTecnicas,
                NumeroParteFabricante = itemInventario.NumeroParteFabricante,
                NumeroSerieLps = itemInventario.NumeroSerieLps,
                Ubicacion = itemInventario.IdUbicacionNavigation.Nombre,
                Estatus = itemInventario.Estatus,
                Clasificacion = itemInventario.IdClasificacionNavigation.Nombre
            };
        }

        public static List<InventarioViewModel> ToViewModelList(List<ItemInventario> itemInventario) =>
            itemInventario.Select(ToViewModel).ToList();
    }
}

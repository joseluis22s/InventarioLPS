using InventarioLPS.Data.Entities;
using InventarioLPS.Models.Inventario;

namespace InventarioLPS.Services.Mappings
{
    public static class InventarioMapping
    {
        public static ItemInventario ToItemInventarioEntity(ItemInventarioViewModel model)
        {
            return new ItemInventario
            {
                CodigoItem = model.CodigoItem,
                ValorUnitarioSinIva = model.ValorUnitarioSinIva,
                Cantidad = model.Cantidad,
                TotalSinIva = model.TotalSinIva,
                CodigoProducto = model.CodigoProducto,
                Departamento = model.Departamento,
                Categoria = model.Categoria,
                LineaServicio = model.LineaServicio,
                SubLineaServicio = model.SubLineaServicio,
                DescripcionEspecifica = model.DescripcionEspecifica,
                EspecificacionesTecnicas = model.EspecificacionesTecnicas,
                NumeroParteFabricante = model.NumeroParteFabricante,
                NumeroSerieLps = model.NumeroSerieLps,
                Estatus = model.Estatus,
                Ubicacion = model.Ubicacion,
                Clasificacion = model.Clasificacion,
                Proveedor = model.Proveedor,
            };
        }
        public static List<ItemInventario> ToItemInventarioEntityList(List<ItemInventarioViewModel> models) =>
            models.Select(ToItemInventarioEntity).ToList();

        public static ItemInventarioViewModel ToItemInventarioViewModel(ItemInventario itemInventario)
        {
            return new ItemInventarioViewModel
            {
                Id = itemInventario.Id,
                CodigoItem = itemInventario.CodigoItem,
                FormaRegistro = itemInventario.IdInformacionRegistroNavigation.IdFormaRegistroNavigation.Nombre,
                NumeroDocumento = itemInventario.IdInformacionRegistroNavigation.NumeroDocumento,
                FechaCompra = itemInventario.IdInformacionRegistroNavigation.FechaCompra,
                ValorUnitarioSinIva = itemInventario.ValorUnitarioSinIva,
                Cantidad = itemInventario.Cantidad,
                TotalSinIva = itemInventario.TotalSinIva,
                Producto = itemInventario.CodigoProductoNavigation.Nombre,
                Departamento = itemInventario.Departamento,
                Categoria = itemInventario.Categoria,
                LineaServicio = itemInventario.LineaServicio,
                SubLineaServicio = itemInventario.SubLineaServicio,
                DescripcionEspecifica = itemInventario.DescripcionEspecifica,
                EspecificacionesTecnicas = itemInventario.EspecificacionesTecnicas,
                NumeroParteFabricante = itemInventario.NumeroParteFabricante,
                NumeroSerieLps = itemInventario.NumeroSerieLps,
                Estatus = itemInventario.Estatus,
                Ubicacion = itemInventario.Ubicacion,
                Clasificacion = itemInventario.Clasificacion,
                Proveedor = itemInventario.Proveedor
            };
        }

        public static List<ItemInventarioViewModel> ToInventarioViewModelList(List<ItemInventario> itemInventario) =>
            itemInventario.Select(ToItemInventarioViewModel).ToList();

        public static InformacionRegistro ToInformacionRegisterEntity(CreateRegistroInventarioViewModel model)
        {
            InformacionRegistro nuevo = new InformacionRegistro
            {
                NumeroDocumento = model.NumeroDocumento,
                MontoTotal = model.MontoTotal,
                IdFormaRegistro = model.FormaRegistroId,
                FechaCompra = model.FechaCompra,
                ItemInventario = ToItemInventarioEntityList(model.Items)
            };
            return nuevo;
        }
    }
}

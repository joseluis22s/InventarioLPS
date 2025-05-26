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
                Departamento = model.Departamento,
                Categoria = model.Categoria,
                LineaServicio = model.LineaServicio,
                SubLineaServicio = model.SubLineaServicio,
                DescripcionEspecifica = model.DescripcionEspecifica,
                EspecificacionesTecnicas = model.EspecificacionesTecnicas,
                ValorSinIva = model.ValorSinIva,
                NumeroParteFabricante = model.NumeroParteFabricante,
                NumeroSerieLps = model.NumeroSerieLps,
                Estatus = model.Estatus,
                IdUbicacion = model.UbicacionId,
                IdClasificacion = model.ClasificacionId,
                CodigoProducto = model.CodigoProducto,
                IdProveedor = model.ProveedorId,
                Cantidad = model.Cantidad
            };
        }
        public static List<ItemInventario> ToItemInventarioEntityList(List<ItemInventarioViewModel> models) =>
            models.Select(ToItemInventarioEntity).ToList();

        public static InventarioViewModel ToViewModel(ItemInventario itemInventario)
        {
            var invs = new InventarioViewModel
            {
                Id = itemInventario.Id,
                CodigoItem = itemInventario.CodigoItem,
                FormaRegistro = itemInventario.IdInformacionRegistroNavigation.IdFormaRegistroNavigation.Nombre,
                NumeroDocumento = itemInventario.IdInformacionRegistroNavigation.NumeroDocumento,
                Departamento = itemInventario.Departamento,
                Categoria = itemInventario.Categoria,
                LineaServicio = itemInventario.LineaServicio,
                SubLineaServicio = itemInventario.SubLineaServicio,
                FechaCompra = itemInventario.IdInformacionRegistroNavigation.FechaCompra,
                ValorSinIva = itemInventario.ValorSinIva,
                Proveedor = itemInventario.IdProveedorNavigation.RazonSocial,
                Producto = itemInventario.CodigoProductoNavigation.Nombre,
                DescripcionEspecifica = itemInventario.DescripcionEspecifica,
                EspecificacionesTecnicas = itemInventario.EspecificacionesTecnicas,
                NumeroParteFabricante = itemInventario.NumeroParteFabricante,
                NumeroSerieLps = itemInventario.NumeroSerieLps,
                Ubicacion = itemInventario.IdUbicacionNavigation.Nombre,
                Estatus = itemInventario.Estatus,
                Clasificacion = itemInventario.IdClasificacionNavigation.Nombre,
                Cantidad = itemInventario.Cantidad
            };
            return invs;
        }

        public static List<InventarioViewModel> ToViewModelList(List<ItemInventario> itemInventario) =>
            itemInventario.Select(ToViewModel).ToList();

        public static InformacionRegistro ToRegisterData (CreateRegistroInventarioViewModel model)
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

namespace InventarioLPS.Models.Inventario
{
    public class InventarioViewModel
    {
        public int Id { get; set; }

        public string CodigoItem { get; set; }

        public string FormaRegistro { get; set; }

        public string NumeroDocumento { get; set; }

        public string Departamento { get; set; }

        public string Categoria { get; set; }

        public string LineaServicio { get; set; }

        public string? SubLineaServicio { get; set; }

        public DateTime FechaCompra { get; set; }

        public decimal ValorSinIva { get; set; }

        public string Proveedor { get; set; }

        public string Producto { get; set; }

        public string DescripcionEspecifica { get; set; }

        public string EspecificacionesTecnicas { get; set; }

        public string NumeroParteFabricante { get; set; }

        public string NumeroSerieLps { get; set; }

        public string Ubicacion { get; set; }

        public string Estatus { get; set; }

        public string Clasificacion { get; set; }

        public int Cantidad { get; set; }
    }
}

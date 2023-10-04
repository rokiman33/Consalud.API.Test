using Consalud.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Model.Dto
{
    // DTOs
    public class DetalleFacturaDTO
    {
        public double? CantidadProducto { get; set; }
        public ProductoDTO? Producto { get; set; }
        public double? TotalProducto { get; set; }
    }

    public class ProductoDTO
    {
        public string? Descripcion { get; set; }
        public double? Valor { get; set; }
    }

    public class FacturasDTO
    {
        public double? NumeroDocumento { get; set; }
        public double? RUTVendedor { get; set; }
        public string? DvVendedor { get; set; }
        public double? RUTComprador { get; set; }
        public string? DvComprador { get; set; }
        public string? RUTCompradorDV { get; set; }
        public string? RUTVendedorDV {  get; set; } 
        public string? DireccionComprador { get; set; }
        public double? ComunaComprador { get; set; }
        public double? RegionComprador { get; set; }
        public double? TotalFactura { get; set; }
        public List<DetalleFacturaDTO>? DetalleFactura { get; set; }
    }


}

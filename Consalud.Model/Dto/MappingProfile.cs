using AutoMapper;
using Consalud.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Model.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DetalleFactura, DetalleFacturaDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Facturas, FacturasDTO>().ReverseMap();
        }
    }
}

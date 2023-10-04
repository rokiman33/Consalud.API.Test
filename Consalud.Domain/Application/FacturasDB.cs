using AutoMapper;
using Consalud.Domain.Application.Services;
using Consalud.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Domain.Application
{
    public class FacturasDB : IFacturasDB
    {
        private readonly IMapper _mapper;
        private readonly FacturaService _facturaService;

        public FacturasDB(IMapper mapper, FacturaService facturaService)
        {
            _mapper = mapper;
            _facturaService = facturaService;
        }

        //Leer archivo json y serializar con clase
        public async Task<List<FacturasDTO>> LoadDB()
        {
            var facturaDto = await _facturaService.LoadFacturaFromJson("JsonEjemplo.json");
            foreach (var factura in facturaDto.ToList())
            {
                factura.TotalFactura = await GetCalcTotalFactura(factura.DetalleFactura.ToList());
            }

            return facturaDto;
        }

        private async Task<double> GetCalcTotalFactura(List<DetalleFacturaDTO> detalleFacturas)
        {
            double totalFactura = 0;

            foreach(var item in  detalleFacturas)
            {
                totalFactura += (double)item.Producto.Valor;
            }

            return totalFactura;
        }



}
}

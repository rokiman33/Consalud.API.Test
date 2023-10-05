using AutoMapper;
using Consalud.Domain.Application.Services;
using Consalud.Model.Dto;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public FacturasDB(IMapper mapper, FacturaService facturaService, IConfiguration configuration)
        {
            _mapper = mapper;
            _facturaService = facturaService;
            _configuration = configuration;
        }

        //Leer archivo json y serializar con clase
        public async Task<List<FacturasDTO>> LoadDB()
        {
            var dbFiles = _configuration["AppFiles:DBFiles"];
            var facturaDto = await _facturaService.LoadFacturaFromJson(dbFiles);
            foreach (var factura in facturaDto.ToList())
            {
                factura.RUTVendedorDV = $"{factura.RUTVendedor}-{factura.DvVendedor}";
                factura.RUTCompradorDV = $"{factura.RUTComprador}-{factura.DvComprador}";
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

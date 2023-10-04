using AutoMapper;
using Consalud.Domain;
using Consalud.Model.Dto;
using Consalud.Model.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Threading;

namespace Consalud.Manager.Services.Impl
{
    public class ReadFacturasImpl: IReadFacturas
    {
        private readonly IMapper _mapper;
        private IFacturasDB _facturasDB;
        private List<FacturasDTO> _facturas = new List<FacturasDTO>();

        public ReadFacturasImpl(IMapper mapper, IFacturasDB facturasDB)
        {
            _mapper = mapper;
            _facturasDB = facturasDB;
        }
        //Retornar lista completa de las facturas y calcular el total de cada para cada una de ellas.
        public async Task<IEnumerable<FacturasDTO>> GetAllFacturas()
        {
            _facturas = await _facturasDB.LoadDB();
            
            return _facturas.Select(f => _mapper.Map<FacturasDTO>(f));
        }

        //Debe retornar las facturas de un comprador según su rut.
        public async Task<IEnumerable<FacturasDTO>> GetFacturasByRUTAsync(string rutComprador)
        {
            _facturas = await _facturasDB.LoadDB();
            return _facturas.Where(f => f.RUTCompradorDV == rutComprador).Select(f => _mapper.Map<FacturasDTO>(f));
        }

        //Debe retornar el comprador que tenga mas compras.
        public async Task<string?> GetTopCompradorByCountAsync()
        {
            _facturas = await _facturasDB.LoadDB();
            var rutbuscado =  _facturas.GroupBy(f => f.RUTCompradorDV)
                           .OrderByDescending(g => g.Count())
                           .FirstOrDefault()?.Key;
            return rutbuscado;

        }

        //Retornar lista de compradores con el monto total de compras realizadas.
        public async Task<List<FacturaRutCompradorDVDto>> GetTotalComprasByCompradorAsync()
        {
            _facturas = await _facturasDB.LoadDB();
            var resultado = (IDictionary<string, double>)_facturas.GroupBy(f => f.RUTCompradorDV)
                           .ToDictionary(g => g.Key, g => g.Sum(f => f.TotalFactura ?? 0));

            List<FacturaRutCompradorDVDto> list = new List<FacturaRutCompradorDVDto>();

            foreach (var values in resultado.ToList())
            {
                FacturaRutCompradorDVDto facturaRut = new FacturaRutCompradorDVDto
                {
                    RutCompradorDV = values.Key,
                    TotalFacturado = values.Value
                };
                list.Add(facturaRut);
            }

            return list;
        }

        //Retornar lista de facturas agrupadas por comuna, y permitir buscar facturas de una comuna específica.
        public async Task<List<FacturasByComunasDto>> GetFacturasByComunaAsync()
        {
            _facturas = await _facturasDB.LoadDB();

            if (_facturas == null)
            {
                return new List<FacturasByComunasDto>();
            }

            var comunas = _facturas
                .Where(f => f.ComunaComprador.HasValue)
                .GroupBy(f => f.ComunaComprador.Value)
                .Select(g => new FacturasByComunasDto
                {
                    ComunaId = g.Key,
                    Facturas = g.Select(f => _mapper.Map<FacturasDTO>(f)).ToList()
                })
                .ToList();

            return comunas;
        }

        public async Task<List<FacturasByComunasDto>> GetFacturasBySpecificComunaAsync(double comuna)
        {
            _facturas = await _facturasDB.LoadDB();

            if (_facturas == null)
            {
                return new List<FacturasByComunasDto>();
            }

            var comunas = _facturas
                .Where(f => f.ComunaComprador == comuna)
                .GroupBy(f => f.ComunaComprador.Value)
                .Select(g => new FacturasByComunasDto
                {
                    ComunaId = g.Key,
                    Facturas = g.Select(f => _mapper.Map<FacturasDTO>(f)).ToList()
                })
                .ToList();

            return comunas;
        }

        
    }
}

using AutoMapper;
using Consalud.Domain;
using Consalud.Model.Dto;
using Consalud.Model.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

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

        public async Task<IEnumerable<FacturasDTO>> GetAllFacturas()
        {
            _facturas = await _facturasDB.LoadDB();
            
            return _facturas.Select(f => _mapper.Map<FacturasDTO>(f));
        }

        public async Task<IEnumerable<FacturasDTO>> GetFacturasByRUTAsync(string rutComprador)
        {
            _facturas = await _facturasDB.LoadDB();
            return _facturas.Where(f => f.RUTCompradorDV == rutComprador).Select(f => _mapper.Map<FacturasDTO>(f));
        }

        public async Task<string?> GetTopCompradorByCountAsync()
        {
            _facturas = await _facturasDB.LoadDB();
            var rutbuscado =  _facturas.GroupBy(f => f.RUTCompradorDV)
                           .OrderByDescending(g => g.Count())
                           .FirstOrDefault()?.Key;
            return rutbuscado;

        }

        public async Task<IDictionary<string, double>> GetTotalComprasByCompradorAsync()
        {
            _facturas = await _facturasDB.LoadDB();
            var resultado = (IDictionary<string, double>)_facturas.GroupBy(f => f.RUTCompradorDV)
                           .ToDictionary(g => g.Key, g => g.Sum(f => f.TotalFactura ?? 0));

            return resultado;
        }

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

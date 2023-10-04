using AutoMapper;
using Consalud.Domain;
using Consalud.Model.Dto;
using Consalud.Model.Model;

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

        public async Task<IEnumerable<FacturasDTO>> GetFacturasByRUTAsync(double rutComprador)
        {
            _facturas = await _facturasDB.LoadDB();
            return _facturas.Where(f => f.RUTComprador == rutComprador).Select(f => _mapper.Map<FacturasDTO>(f));
        }

        public async Task<double?> GetTopCompradorByCountAsync()
        {
            _facturas = await _facturasDB.LoadDB();
            return _facturas.GroupBy(f => f.RUTComprador)
                           .OrderByDescending(g => g.Count())
                           .FirstOrDefault()?.Key;
        }

        public async Task<IDictionary<double, double>> GetTotalComprasByCompradorAsync()
        {
            _facturas = await _facturasDB.LoadDB();
            return (IDictionary<double, double>)_facturas.GroupBy(f => f.RUTComprador)
                           .ToDictionary(g => g.Key, g => g.Sum(f => f.TotalFactura ?? 0));
        }

        public async Task<IDictionary<double, List<FacturasDTO>>> GetFacturasByComunaAsync()
        {
            _facturas = await _facturasDB.LoadDB();
            return (IDictionary<double, List<FacturasDTO>>)_facturas.GroupBy(f => f.ComunaComprador)
                           .ToDictionary(g => g.Key, g => g.Select(f => _mapper.Map<FacturasDTO>(f)).ToList());
        }

        public async Task<List<FacturasDTO>> GetFacturasBySpecificComunaAsync(double comuna)
        {
            _facturas = await _facturasDB.LoadDB();
            return _facturas.Where(f => f.ComunaComprador == comuna).Select(f => _mapper.Map<FacturasDTO>(f)).ToList();
        }


    }
}

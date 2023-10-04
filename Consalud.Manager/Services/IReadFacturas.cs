using Consalud.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Manager.Services
{
    public interface IReadFacturas
    {
        Task<IEnumerable<FacturasDTO>> GetAllFacturas();
        Task<IEnumerable<FacturasDTO>> GetFacturasByRUTAsync(double rutComprador);
        Task<double?> GetTopCompradorByCountAsync();
        Task<IDictionary<double, double>> GetTotalComprasByCompradorAsync();
        Task<IDictionary<double, List<FacturasDTO>>> GetFacturasByComunaAsync();
        Task<List<FacturasDTO>> GetFacturasBySpecificComunaAsync(double comuna);

     }
        
}

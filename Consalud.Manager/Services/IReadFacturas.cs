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
        Task<IEnumerable<FacturasDTO>> GetFacturasByRUTAsync(string rutComprador);
        Task<string?> GetTopCompradorByCountAsync();
        Task<List<FacturaRutCompradorDVDto>> GetTotalComprasByCompradorAsync();
        Task<List<FacturasByComunasDto>> GetFacturasByComunaAsync();
        Task<List<FacturasByComunasDto>> GetFacturasBySpecificComunaAsync(double comuna);

     }
        
}

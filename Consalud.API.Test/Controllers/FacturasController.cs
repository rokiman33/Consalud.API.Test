using Consalud.Domain.Application;
using Consalud.Domain.Application.Services;
using Consalud.Manager.Services;
using Consalud.Manager.Utility;
using Consalud.Model.Dto;
using Consalud.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Consalud.API.Test.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {

        private readonly IReadFacturas _readFacturas;
        

        public FacturasController(IReadFacturas readFacturas)
        {
            _readFacturas = readFacturas;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFacturas()
        {
            var facturas = await _readFacturas.GetAllFacturas();
            return Ok(new APIResponse(ResponseCode.SUCCESS, "Record Found", facturas));
        }

        [HttpGet("byRUT/{rutComprador}")]
        public async Task<IActionResult> GetFacturasByRUT(string rutComprador)
        {
            var facturas = await _readFacturas.GetFacturasByRUTAsync(rutComprador);
            return Ok(new APIResponse(ResponseCode.SUCCESS, "Record Found", facturas));
        }

        [HttpGet("topComprador")]
        public async Task<IActionResult> GetTopCompradorByCount()
        {
            var topCompradorRUT = await _readFacturas.GetTopCompradorByCountAsync();
            if (!string.IsNullOrEmpty(topCompradorRUT))
                return Ok(new APIResponse(ResponseCode.SUCCESS, "Record Found", topCompradorRUT));
            else
                return NotFound(new APIResponse(ResponseCode.SUCCESS,"No se encontró ningún comprador."));
        }

        [HttpGet("comprasByComprador")]
        public async Task<IActionResult> GetTotalComprasByComprador()
        {
            var comprasByComprador = await _readFacturas.GetTotalComprasByCompradorAsync();
            if (comprasByComprador != null)
              return Ok(new APIResponse(ResponseCode.SUCCESS, "Record Found", comprasByComprador));
            else
              return NotFound(new APIResponse(ResponseCode.SUCCESS, "No se encontró Informacion"));
        }

        [HttpGet("byComuna")]
        public async Task<IActionResult> GetFacturasByComuna(double comuna)
        {
            if(comuna.Equals(0))
            {
                var facturasByComuna = await _readFacturas.GetFacturasByComunaAsync();
                return Ok(new APIResponse(ResponseCode.SUCCESS, "Record Found", facturasByComuna));
            }
            else
            {
                var facturas = await _readFacturas.GetFacturasBySpecificComunaAsync(comuna);
                if (facturas.Any())
                    return Ok(new APIResponse(ResponseCode.SUCCESS, "Record Found", facturas));
                else
                    return NotFound(new APIResponse(ResponseCode.SUCCESS, $"No se encontraron facturas para la comuna {comuna}."));
            }
        }

    }
}

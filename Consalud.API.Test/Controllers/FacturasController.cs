﻿using Consalud.Domain.Application.Services;
using Consalud.Manager.Services;
using Microsoft.AspNetCore.Mvc;


namespace Consalud.API.Test.Controllers
{
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
            return Ok(facturas);
        }

        [HttpGet("byRUT/{rutComprador}")]
        public async Task<IActionResult> GetFacturasByRUT(double rutComprador)
        {
            var facturas = await _readFacturas.GetFacturasByRUTAsync(rutComprador);
            return Ok(facturas);
        }

        [HttpGet("topComprador")]
        public async Task<IActionResult> GetTopCompradorByCount()
        {
            var topCompradorRUT = await _readFacturas.GetTopCompradorByCountAsync();
            if (topCompradorRUT.HasValue)
                return Ok(topCompradorRUT);
            else
                return NotFound("No se encontró ningún comprador.");
        }

        [HttpGet("comprasByComprador")]
        public async Task<IActionResult> GetTotalComprasByComprador()
        {
            var comprasByComprador = await _readFacturas.GetTotalComprasByCompradorAsync();
            return Ok(comprasByComprador);
        }

        [HttpGet("byComuna")]
        public async Task<IActionResult> GetFacturasByComuna()
        {
            var facturasByComuna = _readFacturas.GetFacturasByComunaAsync();
            return Ok(facturasByComuna);
        }

        [HttpGet("bySpecificComuna/{comuna}")]
        public async Task<IActionResult> GetFacturasBySpecificComuna(double comuna)
        {
            var facturas = await _readFacturas.GetFacturasBySpecificComunaAsync(comuna);
            if (facturas.Any())
                return Ok(facturas);
            else
                return NotFound($"No se encontraron facturas para la comuna {comuna}.");
        }

    }
}
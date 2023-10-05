using Xunit;
using Moq;
using Consalud.Manager.Services;
using Consalud.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Consalud.API.Test.Controllers;
using Consalud.Manager.Utility;
using Consalud.Model.Model;

namespace Consalud.Test
{
    public class FacturasControllerTest
    {
        private readonly FacturasController _controller;
        private readonly Mock<IReadFacturas> _mockService;

        public FacturasControllerTest()
        {
            _mockService = new Mock<IReadFacturas>();
            _controller = new FacturasController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllFacturas_ReturnsOkResult_WithFacturas()
        {
            // Arrange
            _mockService.Setup(s => s.GetAllFacturas())
                .ReturnsAsync(new List<FacturasDTO> { new FacturasDTO { RUTCompradorDV = "21595854-k", TotalFactura = 5000 }});

            // Act
            var result = await _controller.GetAllFacturas();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as APIResponse;
            Assert.NotNull(response);
            Assert.Equal(ResponseCode.SUCCESS, response.Code);
            var facturasResponse = response.Result as List<FacturasDTO>;
            Assert.Single(facturasResponse);
        }

        [Fact]
        public async Task GetFacturasByRUT_ReturnsFacturasForGivenRUT()
        {
            // Arrange
            string testRUT = "21595854-k";
            List<FacturasDTO> testFacturas = new List<FacturasDTO> { new FacturasDTO() };
            _mockService.Setup(s => s.GetFacturasByRUTAsync(testRUT)).ReturnsAsync(testFacturas);

            // Act
            var result = await _controller.GetFacturasByRUT(testRUT);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as APIResponse;
            Assert.NotNull(response);
            Assert.Equal(ResponseCode.SUCCESS, response.Code);
            var facturasResponse = response.Result as List<FacturasDTO>;
            Assert.Single(facturasResponse);
        }

        [Fact]
        public async Task GetTopCompradorByCount_ReturnsTopComprador()
        {
            // Arrange
            string testTopCompradorRUT = "21595854-k";
            _mockService.Setup(s => s.GetTopCompradorByCountAsync()).ReturnsAsync(testTopCompradorRUT);

            // Act
            var result = await _controller.GetTopCompradorByCount();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as APIResponse;
            Assert.NotNull(response);
            Assert.Equal(ResponseCode.SUCCESS, response.Code);
            var topRUT = response.Result as string;
            Assert.Equal(testTopCompradorRUT, topRUT);
        }

        [Fact]
        public async Task GetTotalComprasByComprador_ReturnsTotalCompras()
        {
            // Arrange
            var totalCompras = new List<FacturaRutCompradorDVDto> { new FacturaRutCompradorDVDto { RutCompradorDV = "21595854-k", TotalFacturado = 100000 } };
            _mockService.Setup(s => s.GetTotalComprasByCompradorAsync()).ReturnsAsync(totalCompras);

            // Act
            var result = await _controller.GetTotalComprasByComprador();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as APIResponse;
            Assert.NotNull(response);
            Assert.Equal(ResponseCode.SUCCESS, response.Code);
            var comprasResponse = response.Result as List<FacturaRutCompradorDVDto>;
            Assert.Single(comprasResponse);
        }

        [Fact]
        public async Task GetFacturasByComuna_WithoutComuna_ReturnsAllFacturasByComuna()
        {
            // Arrange
            var facturasByComuna = new List<FacturasByComunasDto> { new FacturasByComunasDto { ComunaId = 65, Facturas = new List<FacturasDTO> { new FacturasDTO { RUTCompradorDV = "21595854-k", TotalFactura = 10000 } } } };
            _mockService.Setup(s => s.GetFacturasByComunaAsync()).ReturnsAsync(facturasByComuna);

            // Act
            var result = await _controller.GetFacturasByComuna(0);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as APIResponse;
            Assert.NotNull(response);
            Assert.Equal(ResponseCode.SUCCESS, response.Code);
            var facturasResponse = response.Result as List<FacturasByComunasDto>;
            Assert.Single(facturasResponse);
        }

        [Fact]
        public async Task GetFacturasByComuna_WithComuna_ReturnsFacturasBySpecificComuna()
        {
            // Arrange
            double testComuna = 1;
            var facturas = new List<FacturasByComunasDto> { new FacturasByComunasDto { ComunaId = 65, Facturas = new List<FacturasDTO> { new FacturasDTO { RUTCompradorDV = "21595854-k", TotalFactura = 10000 } } } };
            _mockService.Setup(s => s.GetFacturasBySpecificComunaAsync(testComuna)).ReturnsAsync(facturas);

            // Act
            var result = await _controller.GetFacturasByComuna(testComuna);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as APIResponse;
            Assert.NotNull(response);
            Assert.Equal(ResponseCode.SUCCESS, response.Code);
            var facturasResponse = response.Result as List<FacturasByComunasDto>;
            Assert.Single(facturasResponse);
        }


    }
}

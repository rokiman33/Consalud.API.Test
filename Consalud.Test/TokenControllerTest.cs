using Consalud.API.Test.Controllers;
using Consalud.Manager.Utility;
using Consalud.Model.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Test
{
    public class TokenControllerTest
    {
        private readonly TokenController _controller;
        private readonly Mock<IOptions<AppSettings>> _mockOptions;

        public TokenControllerTest()
        {
            // Setup mock options
            AppSettings testSettings = new AppSettings
            {
                Secret = "wkfgKr9jr03uF1ywFHH0uwbInhtCcHVRUrYopPp7dyY",
                TokenValidityDay = 1,
                DefaultTokenUsername = "admin",
                DefaultTokenPassword = "Admin.1234"
            };
            _mockOptions = new Mock<IOptions<AppSettings>>();
            _mockOptions.Setup(x => x.Value).Returns(testSettings);

            _controller = new TokenController(_mockOptions.Object);
        }

        [Fact]
        public void Post_ValidCredentials_ReturnsToken()
        {
            // Arrange
            AuthenticateModel model = new AuthenticateModel
            {
                Username = "admin",
                Password = "Admin.1234"
            };

            // Act
            var result = _controller.Post(model);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var response = okResult.Value as APIResponse;
            Assert.NotNull(response);
            Assert.Equal(ResponseCode.SUCCESS, response.Code);
            var tokenResponse = response.Result as TokenResponse;
            Assert.NotNull(tokenResponse);
            Assert.False(string.IsNullOrEmpty(tokenResponse.AccessToken));
        }
    }
}

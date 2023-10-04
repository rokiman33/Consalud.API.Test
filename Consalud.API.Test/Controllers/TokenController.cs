using Consalud.Manager.Utility;
using Consalud.Model.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Consalud.API.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly AppSettings _appSettings;

        public TokenController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Post(AuthenticateModel model)
        {
            try
            {
                var defaultusername = _appSettings.DefaultTokenUsername;
                var defaultpassword = _appSettings.DefaultTokenPassword;

                if(model.Username == defaultusername && model.Password == defaultpassword)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var day = _appSettings.TokenValidityDay;
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] { 
                           new Claim(ClaimTypes.Name, model.Username.ToString()),
                           new Claim(ClaimTypes.Name, "CustomObjectCanBeAddedHere")
                        }),
                        Expires = DateTime.UtcNow.AddDays(day),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    if (token != null)
                    {
                        TokenResponse tokenResponse = new TokenResponse()
                        {
                            AccessToken = tokenHandler.WriteToken(token),
                            ValidFrom = token.ValidFrom,
                            ValidTo = token.ValidTo,
                        };

                        return Ok(new APIResponse(ResponseCode.SUCCESS, "Bearer Token Generated", tokenResponse));

                    }
                }

                return StatusCode(401, new APIResponse(ResponseCode.ERROR, "Invalid Request"));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse(ResponseCode.ERROR, ex.Message));
            }
        }

    }
}

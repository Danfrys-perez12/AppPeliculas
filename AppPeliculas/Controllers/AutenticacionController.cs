using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using AppPeliculas.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AppPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        //crear para que user
        //pueda registrar y ver si es permitodo
        private readonly string secretKey;

        public AutenticacionController(IConfiguration config) {

            secretKey= config.GetSection("Settings")
                .GetSection("SecretKey").ToString();
        }

        [HttpPost]
        [ Route ("Validar")]
        public IActionResult Validar ([FromBody] Usuarios request)
        {
            if(request.correo=="n@gmail.com"
                && request.clave== "123")
            {
                var KeyBaytes= Encoding.ASCII.GetBytes
                    (secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim( new Claim(ClaimTypes.NameIdentifier,request.correo));

                var TokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject= claims,
                    Expires=DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials= new SigningCredentials
                    ( new SymmetricSecurityKey(KeyBaytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(TokenDescriptor);

                string toKencreado= tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK,
                    new {token=toKencreado});
            }
            else
            {

                return StatusCode(StatusCodes.Status401Unauthorized,
                    new { token = " " });
            }
        }


    }
}

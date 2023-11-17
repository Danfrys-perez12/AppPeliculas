using AppPeliculas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppPeliculas.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly AppPeliculasContext _User;
        private readonly string secretKey;

        public UsersController(AppPeliculasContext user)
        {
            
            _User = user;
            
        }

        [HttpPost]
        [Route("Registrar")]

        public IActionResult Guarda([FromBody] Usuario usuario)
        {
            _User.Usuarios.Add(usuario);
            _User.SaveChanges();

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario Registrado con Exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status203NonAuthoritative, new { mensaje = ex.Message });
            }

        }



        [HttpDelete]
        [Route("Eliminar/{Id_User:int}")]
        public IActionResult Eliminar(int Id_User)
        {
            Usuario usuario = _User.Usuarios.Find(Id_User);

            if (usuario == null)
            {
                return BadRequest("No se ha encontro el usuario");
            }
            try
            {


                _User.Usuarios.Remove(usuario);
                _User.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario Eliminado Con Exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new { mensaje = ex.Message });
            }
        }


    }

}

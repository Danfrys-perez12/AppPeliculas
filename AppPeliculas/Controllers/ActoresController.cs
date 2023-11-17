using AppPeliculas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apii_Moviiees.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        public readonly AppPeliculasContext _dbContext;

        public ActoresController(AppPeliculasContext _Context)
        {
            _dbContext = _Context;
        }


        //Api listar para actores
        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista()
        {
            List<Actores> lista = new List<Actores>();

            try
            {
                lista = _dbContext.Actores.Include(
             a => a.OPeliculas).ToList();

                return StatusCode(
                    StatusCodes.Status200OK,
                    new
                    {
                        mensaje = "ok, hasta el momento",
                        Response = lista
                    }

                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status200OK,
                    new
                    {
                        mensaje = ex.Message,
                        Response = lista
                    }

                    );
            }
        }

        //obtener productos por id
        [HttpGet]
        [Route("Obtener/{Id_Actor:int}")]
        public IActionResult Obtener(int IdActor)
        {
            //List<Actores> lista = new List<Actores>();
            Actores oActores = _dbContext.Actores.Find(IdActor);
            if (oActores == null)
            {
                return BadRequest("No se encontraron Actores");

            }
                

            try
            {
                oActores = _dbContext.Actores.
                    Include(c => c.OPeliculas).
                    Where(p => 
                    p.IdActor == IdActor).
                    FirstOrDefault();


                return StatusCode(
                    StatusCodes.Status200OK,
                    new
                    {
                        mensaje = "ok, hasta el momento",
                        Response = oActores
                    }

                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status200OK,
                    new
                    {
                        mensaje = ex.Message,
                        Response = oActores
                    }

                    );
            }
        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Actores Objeto_Actor)
        {
            try
            {
                _dbContext.Actores.Add(Objeto_Actor);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Actores Agregado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }


      //  Api Editar
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Actores Objeto_Actor)
        {

            Actores oActores = _dbContext.Actores.Find(Objeto_Actor.IdActor);

            if (oActores == null)
            {
                return BadRequest("No se ha encontrado el Actor");
            }
            try
            {
                // ES POR SI UN CAMPO SE DEJA VACIO NO RETORNE IGUAL, MAS BIEN CON LA INFORMACION QUE YA TIENE EL PRODUCTO
                oActores.Nombre = Objeto_Actor.Nombre is null ? oActores.Nombre : Objeto_Actor.Nombre;
                oActores.Apellido = Objeto_Actor.Apellido is null ? oActores.Apellido : Objeto_Actor.Apellido;
                oActores.FechaNacimiento= Objeto_Actor.FechaNacimiento is null ? oActores.FechaNacimiento : Objeto_Actor.FechaNacimiento;
                oActores.Descripcion = Objeto_Actor.Descripcion is null ? oActores.Descripcion : Objeto_Actor.Descripcion;
                oActores.IdPelicula = Objeto_Actor.IdPelicula is null ? oActores.IdPelicula : Objeto_Actor.IdPelicula;
                oActores.Pelicula = Objeto_Actor.Pelicula is null ? oActores.Pelicula : Objeto_Actor.Pelicula;


                _dbContext.Actores.Update(oActores);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Actor Editado Con Exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }


        //Api eliminar
        [HttpDelete]
        [Route("Eliminar/{Id_Actor:int}")]
        public IActionResult Eliminar(int IdActor)
        {
            Actores oActores = _dbContext.Actores.Find(IdActor);

            if (oActores == null)
            {
                return BadRequest("No se ha encontrado el Actor");
            }
            try
            {


                _dbContext.Actores.Remove(oActores);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Actor Eliminado Con Exito" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


    }
}

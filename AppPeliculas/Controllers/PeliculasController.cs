using AppPeliculas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPeliculas.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class PeliculasController : ControllerBase
{
    //declaramos un objeto
    public readonly AppPeliculasContext _Context;
    //contructor
    public PeliculasController(AppPeliculasContext _context)
    {
        _Context = _context;
    }
    //Api listar para actores
    [HttpGet]
    [Route("Lista_Pelicula")]

    public IActionResult Lista()
    {
        List<Pelicula> lista = new List<Pelicula>();

        try
        {
            lista = _Context.Peliculas.ToList();

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

    //obtener Pelicula por id
    [HttpGet]
    [Route("Obtener/{Id_Pelicula:int}")]
    public IActionResult Obtener(int IdPelicula)
    {
        //List<Actores> lista = new List<Actores>();
        Pelicula pelicula = _Context.Peliculas.Find(IdPelicula);
        if (pelicula == null)
        {
            return BadRequest("No se encontro la Pelicula");
        }


        try
        {
            pelicula = _Context.Peliculas.
                Where(p =>
                p.IdPelicula == IdPelicula).
                FirstOrDefault();


            return StatusCode(
                StatusCodes.Status200OK,
                new
                {
                    mensaje = "ok, hasta el momento",
                    Response = pelicula
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
                    Response = pelicula
                }

                );
        }
    }

    [HttpPost]
    [Route("Guardar")]

    public IActionResult Guardar([FromBody] Pelicula Obje_pe)
    {
        try
        {
            _Context.Peliculas.Add(Obje_pe);
            _Context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Pelicula Agregada" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
        }

    }


    //  Api Editar
    [HttpPut]
    [Route("Editar")]
    public IActionResult Editar([FromBody] Pelicula Obje_pe)
    {

        Pelicula pelicula = _Context.Peliculas.Find(Obje_pe.IdPelicula);

        if (pelicula == null)
        {
            return BadRequest("No se ha encontrado la pelicula");
        }
        try
        {
            // ES POR SI UN CAMPO SE DEJA VACIO NO RETORNE IGUAL, MAS BIEN CON LA INFORMACION QUE YA TIENE EL PRODUCTO
            pelicula.Titulo = Obje_pe.Titulo is null ? pelicula.Titulo : Obje_pe.Titulo;
            pelicula.Estreno = Obje_pe.Estreno == null ? pelicula.Estreno : Obje_pe.Estreno;
            pelicula.Actor=Obje_pe.Actor is null ? pelicula.Actor : Obje_pe.Actor;

            


            _Context.Peliculas.Update(pelicula);
            _Context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Pelicula Editada Con Exito" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
        }

    }


    //Api eliminar
    [HttpDelete]
    [Route("Eliminar/{Id_Pelicula:int}")]
    public IActionResult Eliminar(int IdPelicula)
    {
        Pelicula pelicula = _Context.Peliculas.Find(IdPelicula);

        if (pelicula == null)
        {
            return BadRequest("No se ha encontro la pelicula");
        }
        try
        {


            _Context.Peliculas.Remove(pelicula);
            _Context.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Pelicula Eliminada Con Exito" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
        }
    }

}

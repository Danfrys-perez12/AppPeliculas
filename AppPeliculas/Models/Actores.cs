using System;
using System.Collections.Generic;

namespace AppPeliculas.Models;

public partial class Actores
{
    public int IdActor { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? FechaNacimiento { get; set; }

    public string? Descripcion { get; set; }

    public int? IdPelicula { get; set; }

    public string? Pelicula { get; set; }

    public virtual Pelicula? OPeliculas { get; set; }
}

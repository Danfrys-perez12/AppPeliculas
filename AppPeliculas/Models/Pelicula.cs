using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppPeliculas.Models;

public partial class Pelicula
{
    public int IdPelicula { get; set; }

    public string? Titulo { get; set; }

    public string? Estreno { get; set; }

    public string? Actor { get; set; }
    [JsonIgnore]
    public virtual ICollection<Actores> Actores { get; set; } = new List<Actores>();
}

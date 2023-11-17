using System;
using System.Collections.Generic;

namespace AppPeliculas.Models;

public partial class Usuario
{
    public int IdUser { get; set; }

    public string? Nombre { get; set; }

    public string? Imail { get; set; }

    public string? Clave { get; set; }
}

using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Insumo> Insumos { get; set; } = new List<Insumo>();
}

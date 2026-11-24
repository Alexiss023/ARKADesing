using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Diseno
{
    public int IdDiseno { get; set; }

    public int IdDetalle { get; set; }

    public int IdUsuario { get; set; }

    public string ArchivoUrl { get; set; } = null!;

    public int Version { get; set; }

    public string EstadoAprobacion { get; set; } = null!;

    public DateTime FechaSubida { get; set; }

    public virtual DetallePedido IdDetalleNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Cotizacion
{
    public int IdCotizacion { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public DateOnly Fecha { get; set; }

    public DateOnly Vigencia { get; set; }

    public string? DescripcionTrabajo { get; set; }

    public decimal Total { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<DetalleCotizacion> DetalleCotizacions { get; set; } = new List<DetalleCotizacion>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Pedido? Pedido { get; set; }
}

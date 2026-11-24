using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class HistorialEstadoPedido
{
    public int IdHistorial { get; set; }

    public int IdPedido { get; set; }

    public int IdUsuario { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string? Observacion { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

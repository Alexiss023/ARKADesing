using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Entrega
{
    public int IdEntrega { get; set; }

    public int IdPedido { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Direccion { get; set; }

    public DateOnly? Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}

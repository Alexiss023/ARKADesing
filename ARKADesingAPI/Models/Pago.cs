using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public int IdPedido { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal Monto { get; set; }

    public string Metodo { get; set; } = null!;

    public string? Referencia { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}

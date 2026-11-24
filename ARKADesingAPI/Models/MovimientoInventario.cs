using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class MovimientoInventario
{
    public int IdMovimiento { get; set; }

    public int IdInsumo { get; set; }

    public int IdUsuario { get; set; }

    public int? IdPedido { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal Cantidad { get; set; }

    public DateTime Fecha { get; set; }

    public string? Motivo { get; set; }

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Pedido? IdPedidoNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public int? IdCotizacion { get; set; }

    public DateOnly Fecha { get; set; }

    public DateOnly? FechaEntregaEstimada { get; set; }

    public string Estado { get; set; } = null!;

    public decimal Total { get; set; }

    public decimal Saldo { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Entrega? Entrega { get; set; }

    public virtual ICollection<HistorialEstadoPedido> HistorialEstadoPedidos { get; set; } = new List<HistorialEstadoPedido>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Cotizacion? IdCotizacionNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}

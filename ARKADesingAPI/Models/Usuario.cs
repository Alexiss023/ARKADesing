using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string ContrasenaHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Cotizacion> Cotizacions { get; set; } = new List<Cotizacion>();

    public virtual ICollection<Diseno> Disenos { get; set; } = new List<Diseno>();

    public virtual ICollection<HistorialEstadoPedido> HistorialEstadoPedidos { get; set; } = new List<HistorialEstadoPedido>();

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}

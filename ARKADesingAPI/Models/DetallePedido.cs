using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class DetallePedido
{
    public int IdDetalle { get; set; }

    public int IdPedido { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public string? NotaPersonalizacion { get; set; }

    public virtual ICollection<Diseno> Disenos { get; set; } = new List<Diseno>();

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}

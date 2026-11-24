using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class ProductoInsumo
{
    public int IdProducto { get; set; }

    public int IdInsumo { get; set; }

    public decimal CantidadPorUnidad { get; set; }

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}

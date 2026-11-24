using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Tecnica { get; set; } = null!;

    public decimal PrecioBase { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<DetalleCotizacion> DetalleCotizacions { get; set; } = new List<DetalleCotizacion>();

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<ProductoInsumo> ProductoInsumos { get; set; } = new List<ProductoInsumo>();
}

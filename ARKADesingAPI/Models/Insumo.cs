using System;
using System.Collections.Generic;

namespace ARKADesingAPI.Models;

public partial class Insumo
{
    public int IdInsumo { get; set; }

    public int IdProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string UnidadMedida { get; set; } = null!;

    public decimal Existencia { get; set; }

    public decimal ExistenciaMinima { get; set; }

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();

    public virtual ICollection<ProductoInsumo> ProductoInsumos { get; set; } = new List<ProductoInsumo>();
}

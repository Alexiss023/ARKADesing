using System.ComponentModel.DataAnnotations;

namespace ARKADesingAPI.DTOs;

// Datos que se reciben al crear o actualizar un insumo.
// Se usa un DTO para no exigir la propiedad de navegación del proveedor
// y para validar los datos antes de llegar a la base de datos.
public class InsumoDto
{
    [Required(ErrorMessage = "El proveedor es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe indicar un proveedor válido.")]
    public int IdProveedor { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La unidad de medida es obligatoria.")]
    [StringLength(20, ErrorMessage = "La unidad de medida no puede superar 20 caracteres.")]
    public string UnidadMedida { get; set; } = null!;

    [Range(0, 99999999.99, ErrorMessage = "La existencia no puede ser negativa.")]
    public decimal Existencia { get; set; }

    [Range(0, 99999999.99, ErrorMessage = "La existencia mínima no puede ser negativa.")]
    public decimal ExistenciaMinima { get; set; }
}

// Datos que se devuelven al consultar insumos (incluye el nombre del proveedor).
public class InsumoRespuestaDto
{
    public int IdInsumo { get; set; }
    public int IdProveedor { get; set; }
    public string Proveedor { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string UnidadMedida { get; set; } = null!;
    public decimal Existencia { get; set; }
    public decimal ExistenciaMinima { get; set; }
    public bool BajoMinimo { get; set; }
}

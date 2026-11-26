using ARKADesingAPI.DTOs;
using ARKADesingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARKADesingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsumosController : ControllerBase
    {
        private readonly ArkaDbContext _context;

        public InsumosController(ArkaDbContext context)
        {
            _context = context;
        }

        // GET: api/Insumos
        // GET: api/Insumos?bajoMinimo=true  -> solo insumos con existencia por debajo del mínimo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsumoRespuestaDto>>> GetInsumos([FromQuery] bool? bajoMinimo)
        {
            var consulta = _context.Insumos.AsNoTracking();

            if (bajoMinimo == true)
            {
                consulta = consulta.Where(i => i.Existencia < i.ExistenciaMinima);
            }

            return await consulta
                .OrderBy(i => i.Nombre)
                .Select(i => new InsumoRespuestaDto
                {
                    IdInsumo = i.IdInsumo,
                    IdProveedor = i.IdProveedor,
                    Proveedor = i.IdProveedorNavigation.Nombre,
                    Nombre = i.Nombre,
                    UnidadMedida = i.UnidadMedida,
                    Existencia = i.Existencia,
                    ExistenciaMinima = i.ExistenciaMinima,
                    BajoMinimo = i.Existencia < i.ExistenciaMinima
                })
                .ToListAsync();
        }

        // GET: api/Insumos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsumoRespuestaDto>> GetInsumo(int id)
        {
            var insumo = await _context.Insumos
                .AsNoTracking()
                .Where(i => i.IdInsumo == id)
                .Select(i => new InsumoRespuestaDto
                {
                    IdInsumo = i.IdInsumo,
                    IdProveedor = i.IdProveedor,
                    Proveedor = i.IdProveedorNavigation.Nombre,
                    Nombre = i.Nombre,
                    UnidadMedida = i.UnidadMedida,
                    Existencia = i.Existencia,
                    ExistenciaMinima = i.ExistenciaMinima,
                    BajoMinimo = i.Existencia < i.ExistenciaMinima
                })
                .FirstOrDefaultAsync();

            if (insumo == null)
            {
                return NotFound(new { mensaje = "Insumo no encontrado." });
            }

            return insumo;
        }

        // POST: api/Insumos
        [HttpPost]
        public async Task<ActionResult<InsumoRespuestaDto>> PostInsumo(InsumoDto dto)
        {
            var proveedor = await _context.Proveedors.FindAsync(dto.IdProveedor);
            if (proveedor == null)
            {
                return BadRequest(new { mensaje = "El proveedor indicado no existe." });
            }

            var insumo = new Insumo
            {
                IdProveedor = dto.IdProveedor,
                Nombre = dto.Nombre.Trim(),
                UnidadMedida = dto.UnidadMedida.Trim(),
                Existencia = dto.Existencia,
                ExistenciaMinima = dto.ExistenciaMinima
            };

            _context.Insumos.Add(insumo);
            await _context.SaveChangesAsync();

            var respuesta = new InsumoRespuestaDto
            {
                IdInsumo = insumo.IdInsumo,
                IdProveedor = insumo.IdProveedor,
                Proveedor = proveedor.Nombre,
                Nombre = insumo.Nombre,
                UnidadMedida = insumo.UnidadMedida,
                Existencia = insumo.Existencia,
                ExistenciaMinima = insumo.ExistenciaMinima,
                BajoMinimo = insumo.Existencia < insumo.ExistenciaMinima
            };

            return CreatedAtAction(nameof(GetInsumo), new { id = insumo.IdInsumo }, respuesta);
        }

        // PUT: api/Insumos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsumo(int id, InsumoDto dto)
        {
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null)
            {
                return NotFound(new { mensaje = "Insumo no encontrado." });
            }

            var proveedorExiste = await _context.Proveedors.AnyAsync(p => p.IdProveedor == dto.IdProveedor);
            if (!proveedorExiste)
            {
                return BadRequest(new { mensaje = "El proveedor indicado no existe." });
            }

            insumo.IdProveedor = dto.IdProveedor;
            insumo.Nombre = dto.Nombre.Trim();
            insumo.UnidadMedida = dto.UnidadMedida.Trim();
            insumo.Existencia = dto.Existencia;
            insumo.ExistenciaMinima = dto.ExistenciaMinima;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Insumos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsumo(int id)
        {
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null)
            {
                return NotFound(new { mensaje = "Insumo no encontrado." });
            }

            try
            {
                _context.Insumos.Remove(insumo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new { mensaje = "No se puede eliminar: el insumo tiene movimientos de inventario o productos asociados." });
            }

            return NoContent();
        }
    }
}

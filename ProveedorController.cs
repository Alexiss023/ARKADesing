using ARKADesingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARKADesingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ArkaDbContext _context;

        public ProveedorController(ArkaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Proveedor>>> GetProveedores()
        {
            return await _context.Proveedors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null) return NotFound();
            return proveedor;
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.IdProveedor }, proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor) return BadRequest();

            var existente = await _context.Proveedors.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nombre = proveedor.Nombre;
            existente.Telefono = proveedor.Telefono;
            existente.Correo = proveedor.Correo;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null) return NotFound();

            try
            {
                _context.Proveedors.Remove(proveedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict("No se puede eliminar: el proveedor tiene insumos asociados.");
            }

            return NoContent();
        }
    }
}
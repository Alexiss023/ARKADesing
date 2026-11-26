using ARKADesingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARKADesingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ArkaDbContext _context;

        public CategoriaController(ArkaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categorium>>> GetCategorias()
        {
            return await _context.Categoria.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categorium>> GetCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null) return NotFound();
            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<Categorium>> PostCategoria(Categorium categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.IdCategoria }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categorium categoria)
        {
            if (id != categoria.IdCategoria) return BadRequest();

            var existente = await _context.Categoria.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nombre = categoria.Nombre;
            existente.Descripcion = categoria.Descripcion;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null) return NotFound();

            try
            {
                _context.Categoria.Remove(categoria);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict("No se puede eliminar: la categoría tiene productos asociados.");
            }

            return NoContent();
        }
    }
}
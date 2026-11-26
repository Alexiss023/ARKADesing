using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ARKADesingAPI.Models;

namespace ARKADesingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ArkaDbContext _context;

        // Ajusta esta lista con los valores exactos de tu constraint CK_Pedido_estado en SQL Server
        private static readonly string[] EstadosValidos =
        {
            "pendiente",
            "en_proceso",
            "listo",
            "entregado",
            "cancelado"
        };

        public PedidosController(ArkaDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound(new { mensaje = $"No se encontró el pedido con ID {id}." });
            }

            return pedido;
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            // Ignoramos validación de objetos de navegación para que solo pida los IDs
            ModelState.Remove(nameof(Pedido.IdClienteNavigation));
            ModelState.Remove(nameof(Pedido.IdUsuarioNavigation));
            ModelState.Remove(nameof(Pedido.IdCotizacionNavigation));

            // Normalizamos el estado (en minúsculas y sin espacios) o asignamos uno válido por defecto
            if (string.IsNullOrWhiteSpace(pedido.Estado) || pedido.Estado == "string")
            {
                pedido.Estado = "listo"; // Cambia esto por tu estado inicial real (ej. "pendiente" o "recibido") si existe en CK_Pedido_estado
            }
            else
            {
                pedido.Estado = pedido.Estado.Trim().ToLower();
            }

            if (!EstadosValidos.Contains(pedido.Estado))
            {
                return BadRequest(new
                {
                    mensaje = $"El estado '{pedido.Estado}' no es válido.",
                    estadosPermitidos = EstadosValidos
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPedido), new { id = pedido.IdPedido }, pedido);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al guardar el pedido en la base de datos.",
                    detalle = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        // PUT: api/Pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del pedido." });
            }

            ModelState.Remove(nameof(Pedido.IdClienteNavigation));
            ModelState.Remove(nameof(Pedido.IdUsuarioNavigation));
            ModelState.Remove(nameof(Pedido.IdCotizacionNavigation));

            if (!string.IsNullOrWhiteSpace(pedido.Estado))
            {
                pedido.Estado = pedido.Estado.Trim().ToLower();
            }

            if (!EstadosValidos.Contains(pedido.Estado))
            {
                return BadRequest(new
                {
                    mensaje = $"El estado '{pedido.Estado}' no es válido.",
                    estadosPermitidos = EstadosValidos
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al actualizar el pedido en la base de datos.",
                    detalle = ex.InnerException?.Message ?? ex.Message
                });
            }

            return NoContent();
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedido == id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRest2.Models;

namespace WebApiRest2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TablaUsuariosController : ControllerBase
    {
        private readonly sistemaLoginContext _context;

        public TablaUsuariosController(sistemaLoginContext context)
        {
            _context = context;
        }

        // GET: api/TablaUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TablaUsuarios>>> GetTablaUsuarios()
        {
            return await _context.TablaUsuarios.ToListAsync();
        }

        // GET: api/TablaUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TablaUsuarios>> GetTablaUsuarios(decimal id)
        {
            var tablaUsuarios = await _context.TablaUsuarios.FindAsync(id);

            if (tablaUsuarios == null)
            {
                return NotFound();
            }

            return tablaUsuarios;
        }

        // PUT: api/TablaUsuarios/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTablaUsuarios(decimal id, TablaUsuarios tablaUsuarios)
        {
            if (id != tablaUsuarios.IdPruenbas)
            {
                return BadRequest();
            }

            _context.Entry(tablaUsuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TablaUsuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TablaUsuarios
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TablaUsuarios>> PostTablaUsuarios(TablaUsuarios tablaUsuarios)
        {
            _context.TablaUsuarios.Add(tablaUsuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTablaUsuarios", new { id = tablaUsuarios.IdPruenbas }, tablaUsuarios);
        }

        // DELETE: api/TablaUsuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TablaUsuarios>> DeleteTablaUsuarios(decimal id)
        {
            var tablaUsuarios = await _context.TablaUsuarios.FindAsync(id);
            if (tablaUsuarios == null)
            {
                return NotFound();
            }

            _context.TablaUsuarios.Remove(tablaUsuarios);
            await _context.SaveChangesAsync();

            return tablaUsuarios;
        }

        private bool TablaUsuariosExists(decimal id)
        {
            return _context.TablaUsuarios.Any(e => e.IdPruenbas == id);
        }
    }
}

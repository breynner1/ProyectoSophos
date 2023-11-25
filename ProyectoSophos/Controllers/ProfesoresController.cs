using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoSophos.DBContext;
using ProyectoSophos.Models;

namespace ProyectoSophos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfesoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesore>>> GetProfesores(
            [FromQuery(Name = "name")] string? name
        )
        {
          if (_context.Profesores == null)
          {
              return NotFound();
          }
            var profesores = await _context.Profesores.ToListAsync();

            if (name != null)
            {
                profesores = profesores.Where(pr => pr.Nombre == name).ToList();
            }

            return profesores;
        }

        // GET: api/Profesores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesore>> GetProfesore(int id)
        {
          if (_context.Profesores == null)
          {
              return NotFound();
          }
            var profesore = await _context.Profesores.FindAsync(id);

            if (profesore == null)
            {
                return NotFound();
            }

            return profesore;
        }

        // PUT: api/Profesores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesore(int id, Profesore profesore)
        {
            if (id != profesore.Id)
            {
                return BadRequest();
            }

            _context.Entry(profesore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesoreExists(id))
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

        // POST: api/Profesores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profesore>> PostProfesore(Profesore profesore)
        {
          if (_context.Profesores == null)
          {
              return Problem("Entity set 'AppDbContext.Profesores'  is null.");
          }
            _context.Profesores.Add(profesore);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfesoreExists(profesore.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProfesore", new { id = profesore.Id }, profesore);
        }

        // DELETE: api/Profesores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesore(int id)
        {
            if (_context.Profesores == null)
            {
                return NotFound();
            }
            var profesore = await _context.Profesores.FindAsync(id);
            if (profesore == null)
            {
                return NotFound();
            }

            _context.Profesores.Remove(profesore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesoreExists(int id)
        {
            return (_context.Profesores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

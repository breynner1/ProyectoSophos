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
    public class CursoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cursoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos(
            [FromQuery(Name = "disponible")] bool? num,
            [FromQuery(Name = "name")] string? name
        )
        {
          if (_context.Cursos == null)
          {
              return NotFound();
          }
            var cursos = await _context.Cursos.ToListAsync();

            if (name != null)
            {
                cursos = cursos.Where(cu => cu.Nombre == name).ToList();
            }
            if (num != null)
            {
                cursos = cursos.Where(cu => (cu.CuposDisponibles > 0)==num).ToList();
            }
            return cursos;
        }

        // GET: api/Cursoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Object>>> GetCurso(int id)
        {
          if (_context.Cursos == null)
          {
              return NotFound();
          }
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }
            var alum = _context.Matriculas.Where(ma => ma.CursoId == curso.Id).Select(ma => ma.Alumno.Nombre).ToList();

            return Ok( new {
                Id = curso.Id,
                Nombre = curso.Nombre,
                NumC = curso.CuposDisponibles,
                Creditos = curso.Creditos,
                Inscritos = _context.Matriculas.Where(ma => ma.CursoId == curso.Id).Count(),
                Profesor = curso.ProfesorId,
                Alumnos = alum
            });   

        }



        // PUT: api/Cursoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest();
            }

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // POST: api/Cursoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
          if (_context.Cursos == null)
          {
              return Problem("Entity set 'AppDbContext.Cursos'  is null.");
          }
            _context.Cursos.Add(curso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CursoExists(curso.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCurso", new { id = curso.Id }, curso);
        }

        // DELETE: api/Cursoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            if (_context.Cursos == null)
            {
                return NotFound();
            }
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            return (_context.Cursos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

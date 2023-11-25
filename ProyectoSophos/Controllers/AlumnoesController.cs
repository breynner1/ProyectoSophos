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
    public class AlumnoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlumnoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Alumnoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos(
            [FromQuery(Name = "name")] string? name,
            [FromQuery(Name = "facultad")] int? facul
        )
        {
            if (_context.Alumnos == null)
            {
                return NotFound();
            }
            var alumnos = await _context.Alumnos.ToListAsync();

            if (facul != null)
            {
                alumnos = alumnos.Where(al => al.FacultadId == facul).ToList();
            }

            if (name != null)
            {
                alumnos = alumnos.Where(al => al.Nombre == name).ToList();
            }

            return alumnos;
        }

        // GET: api/Alumnoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Object>>> GetAlumno(int id)
        {
            if (_context.Alumnos == null)
            {
                return NotFound();
            }
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }
             var cur = _context.Matriculas.Where(ma => ma.AlumnoId == alumno.Id).Select(ma => ma.Curso);   
            return Ok(new
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                NumC = alumno.CreditosInscritos,
                Creditos = alumno.Semestre,
                cursosActivos = cur.Where(cu => cu.Estado == true).Select(cur=> cur.Nombre).ToList(),
                cursosCursados = cur.Where(cu => cu.Estado == false).Select(cur => cur.Nombre).ToList()
            }); ;
        }


        // PUT: api/Alumnoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return BadRequest();
            }

            _context.Entry(alumno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
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

        // POST: api/Alumnoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno)
        {
            if (_context.Alumnos == null)
            {
                return Problem("Entity set 'AppDbContext.Alumnos'  is null.");
            }
            _context.Alumnos.Add(alumno);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlumnoExists(alumno.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAlumno", new { id = alumno.Id }, alumno);
        }

        // DELETE: api/Alumnoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            if (_context.Alumnos == null)
            {
                return NotFound();
            }
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlumnoExists(int id)
        {
            return (_context.Alumnos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

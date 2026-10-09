
using denauncebeast2.API.Data;
using denauncebeast2.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace denauncebeast2.API.Controllers
{
    [ApiController]
    [Route("api/sectors")]
    public class SectorsController : ControllerBase
    {
        private readonly DataContext _context;

        public SectorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sector>>> GetAll()
        {
            var sectors = await _context.Sectors.ToListAsync();
            return Ok(sectors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sector>> GetById(int id)
        {
            var sector = await _context.Sectors.FindAsync(id);
            if (sector == null)
            {
                return NotFound();
            }
            return Ok(sector);
        }

        [HttpPost]
        public async Task<ActionResult<Sector>> Create(Sector sector)
        {
            if (string.IsNullOrWhiteSpace(sector.Name))
            {
                return BadRequest("Name of sector is required.");
            }

            // Validar que el municipio exista en la base de datos antes de asociarlo
            var municipalityExists = await _context.Municipalities.AnyAsync(m => m.Id == sector.MunicipalityId && m.IsActive);
            if (!municipalityExists)
            {
                return BadRequest("The specified municipality does not exist or is inactive.");
            }

            sector.IsActive = true;

            _context.Sectors.Add(sector);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = sector.Id },
                sector
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Sector sector)
        {
            var existing = await _context.Sectors.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            var municipalityExists = await _context.Municipalities.AnyAsync(m => m.Id == sector.MunicipalityId && m.IsActive);
            if (!municipalityExists)
            {
                return BadRequest("The specified municipality does not exist or is inactive.");
            }

            existing.Name = sector.Name;
            existing.MunicipalityId = sector.MunicipalityId;
            existing.IsActive = sector.IsActive;

            _context.Sectors.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Sectors.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            _context.Sectors.Remove(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
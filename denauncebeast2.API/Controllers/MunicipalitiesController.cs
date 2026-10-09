using denauncebeast2.API.Data;
using denauncebeast2.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace denauncebeast2.API.Controllers
{
    [ApiController]
    [Route("api/municipalities")]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public MunicipalitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Municipality>>> GetAll()
        {
            var municipalities = await _context.Municipalities.ToListAsync();
            return Ok(municipalities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Municipality>> GetById(int id)
        {
            var municipality = await _context.Municipalities.FindAsync(id);
            if (municipality == null)
            {
                return NotFound();
            }
            return Ok(municipality);
        }

        [HttpPost]
        public async Task<ActionResult<Municipality>> Create(Municipality municipality)
        {
            if (string.IsNullOrWhiteSpace(municipality.Name))
            {
                return BadRequest("Name of municipality is required.");
            }

            municipality.IsActive = true;

            _context.Municipalities.Add(municipality);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = municipality.Id },
                municipality
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Municipality municipality)
        {
            var existing = await _context.Municipalities.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = municipality.Name;
            existing.PostalCode = municipality.PostalCode;
            existing.IsActive = municipality.IsActive;

            _context.Municipalities.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Municipalities.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            _context.Municipalities.Remove(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
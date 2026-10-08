using denauncebeast2.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace denauncebeast2.API.Controllers
{
    [ApiController]
    [Route("api/municipalities")]
    public class MunicipalitiesController : ControllerBase
    {
        private static readonly List<Municipality> _municipalities = new List<Municipality>
        {
            new Municipality { Id = 1, Name = "Santo Domingo", PostalCode = "10101", IsActive = true },
            new Municipality { Id = 2, Name = "Santiago de los Caballeros", PostalCode = "51000", IsActive = true },
            new Municipality { Id = 3, Name = "Puerto Plata", PostalCode = "57000", IsActive = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Municipality>> GetAll()
        {
            return Ok(_municipalities);
        }

        [HttpGet("{id}")]
        public ActionResult<Municipality> GetById(int id)
        {
            var municipality = _municipalities.FirstOrDefault(m => m.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }
            return Ok(municipality);
        }

        [HttpPost]
        public ActionResult<Municipality> Create(Municipality municipality)
        {
            if (string.IsNullOrWhiteSpace(municipality.Name))
            {
                return BadRequest("Name of municipality is required.");
            }

            int newId = _municipalities.Any() ? _municipalities.Max(m => m.Id) + 1 : 1;
            municipality.Id = newId;
            municipality.IsActive = true;

            _municipalities.Add(municipality);

            return CreatedAtAction(
                nameof(GetById),
                new { id = municipality.Id },
                municipality
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Municipality municipality)
        {
            var existing = _municipalities.FirstOrDefault(m => m.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = municipality.Name;
            existing.PostalCode = municipality.PostalCode;
            existing.IsActive = municipality.IsActive;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _municipalities.FirstOrDefault(m => m.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            _municipalities.Remove(existing);
            return NoContent();
        }

        public static bool Exists(int id)
        {
            return _municipalities.Any(m => m.Id == id && m.IsActive);
        }

        public static string GetMunicipalityName(int id)
        {
            var mun = _municipalities.FirstOrDefault(m => m.Id == id);
            return mun != null ? mun.Name : "Desconocido";
        }
    }
}
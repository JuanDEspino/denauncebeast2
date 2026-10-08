
using denauncebeast2.API.Models.DTOs;
using denauncebeast2.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace denauncebeast2.API.Controllers
{
    [ApiController]
    [Route("api/sectors")]
    public class SectorsController : ControllerBase
    {
        private static readonly List<Sector> _sectors = new List<Sector>
        {
            new Sector { Id = 1, Name = "Zona Colonial", MunicipalityId = 1, IsActive = true },
            new Sector { Id = 2, Name = "Gascue", MunicipalityId = 1, IsActive = true },
            new Sector { Id = 3, Name = "Cienfuegos", MunicipalityId = 2, IsActive = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<SectorDto>> GetAll()
        {
            var response = _sectors.Select(s => new SectorDto
            {
                Id = s.Id,
                Name = s.Name,
                MunicipalityId = s.MunicipalityId,
                MunicipalityName = MunicipalitiesController.GetMunicipalityName(s.MunicipalityId),
                IsActive = s.IsActive
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<SectorDto> GetById(int id)
        {
            var sector = _sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }

            var dto = new SectorDto
            {
                Id = sector.Id,
                Name = sector.Name,
                MunicipalityId = sector.MunicipalityId,
                MunicipalityName = MunicipalitiesController.GetMunicipalityName(sector.MunicipalityId),
                IsActive = sector.IsActive
            };

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<SectorDto> Create(CreateSectorDto createDto)
        {
            // Validar que el municipio exista
            if (!MunicipalitiesController.Exists(createDto.MunicipalityId))
            {
                return BadRequest($"El municipio con ID {createDto.MunicipalityId} no existe.");
            }

            int newId = _sectors.Any() ? _sectors.Max(s => s.Id) + 1 : 1;

            var sector = new Sector
            {
                Id = newId,
                Name = createDto.Name,
                MunicipalityId = createDto.MunicipalityId,
                IsActive = true
            };

            _sectors.Add(sector);

            var sectorDto = new SectorDto
            {
                Id = sector.Id,
                Name = sector.Name,
                MunicipalityId = sector.MunicipalityId,
                MunicipalityName = MunicipalitiesController.GetMunicipalityName(sector.MunicipalityId),
                IsActive = sector.IsActive
            };

            return CreatedAtAction(nameof(GetById), new { id = sector.Id }, sectorDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateSectorDto updateDto)
        {
            var existing = _sectors.FirstOrDefault(s => s.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            if (!MunicipalitiesController.Exists(updateDto.MunicipalityId))
            {
                return BadRequest($"El municipio con ID {updateDto.MunicipalityId} no existe.");
            }

            existing.Name = updateDto.Name;
            existing.MunicipalityId = updateDto.MunicipalityId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _sectors.FirstOrDefault(s => s.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            _sectors.Remove(existing);
            return NoContent();
        }
    }
}
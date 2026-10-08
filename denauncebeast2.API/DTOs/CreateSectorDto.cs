using System.ComponentModel.DataAnnotations;

namespace denauncebeast2.API.Models.DTOs
{
    public class CreateSectorDto
    {
        [Required(ErrorMessage = "El nombre del sector es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ID del municipio es obligatorio.")]
        public int MunicipalityId { get; set; }
    }
}
namespace denauncebeast2.API.Models.DTOs
{
    public class SectorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}

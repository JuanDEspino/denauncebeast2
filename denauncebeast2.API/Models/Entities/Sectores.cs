namespace denauncebeast2.API.Models.Entities
{
 feature-4-relacion-sectores
    public class Sector
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MunicipalityId { get; set; }
        public bool IsActive { get; set; } = true;
    }

        public class Sector
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int MunicipalityId { get; set; }
            public bool IsActive { get; set; } = true;
        }
main
}

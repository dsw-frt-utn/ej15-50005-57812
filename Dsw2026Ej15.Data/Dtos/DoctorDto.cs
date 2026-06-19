namespace Dsw2026Ej15.Data.Dtos;

    internal record DoctorDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid SpecilityId { get; set; }
    }

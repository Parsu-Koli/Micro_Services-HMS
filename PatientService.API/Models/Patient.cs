namespace PatientService.API.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; }
        public required int Age { get; set; }

        public required string Phone { get; set; }
        public required string Address { get; set; }

        public string? BloodGroup { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}

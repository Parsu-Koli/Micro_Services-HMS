using System.ComponentModel;

namespace AuthServices.API.Models
{
    public class DoctorRegister
    {
       
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public required string Specialization { get; set; }
        public required string Department { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        [PasswordPropertyText]
        public required string Password { get; set; }
    }
}

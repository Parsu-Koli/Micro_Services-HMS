using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServices.API.Models
{
    public class PatientRegister
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Gender {get; set;}
        public required int Age { get; set; }
        public required string Phone {  get; set; }
        public required string Address {  get; set; }
        public required string Password {  get; set; }
    }
}

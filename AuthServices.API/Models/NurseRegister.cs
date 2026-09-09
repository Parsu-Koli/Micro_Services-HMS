using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServices.API.Models
{
    public class NurseRegister
    {
        
        
        public required string FullName { get; set; }
        public required string UserName {  get; set; }
        public required string Email { get; set; }
        public required string Department { get; set; }
        public required string Shift { get; set; }
        public required string Address {  get; set; }
        public required string Password {  get; set; }
    }
}

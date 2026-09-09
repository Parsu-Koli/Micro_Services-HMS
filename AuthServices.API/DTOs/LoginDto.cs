using System.ComponentModel;

namespace AuthServices.API.DTOs
{
    public class LoginDto
    {
        public required string Email {  get; set; }
        [PasswordPropertyText]
        public required string Password { get; set; }
    }
}

using AuthServices.API.DTOs;
using AuthServices.API.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServices.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        private IDbConnection Connection =>
            new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

       
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> AllLogin([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                using var db = Connection;

                var user = await db.QueryFirstOrDefaultAsync<LoginResponse>(
                    "sp_LoginUser",
                    new { dto.Email, dto.Password },
                    commandType: CommandType.StoredProcedure
                );

                if (user == null)
                    return Unauthorized("Invalid email or password");

                var token = GenerateJwtToken(user);

                return Ok(new
                {
                    user.UserId,
                    user.UserName,
                    user.Role,
                    Token = token
                });
            }
            catch (SqlException)
            {
                return StatusCode(500, "Database error occurred.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong. Please try again.");
            }
        }

        
       

        
        

        private string GenerateJwtToken(LoginResponse user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

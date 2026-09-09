using Dapper;
using DoctorService.API.Models;
using DoctorService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DoctorService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController(DoctorServices _services) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var result = await _services.GetAllDoctors();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            try
            {
                var result = await _services.GetDoctorById(id);
                return Ok(result);
            }


            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody]Doctor doctor)
        {
            try
            {
                await _services.CreateDoctor(doctor);
                return Ok(new { message = "Doctor created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {message =  ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(500,new {message = ex.Message});
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateDoctor(int id,[FromBody]Doctor doctor)
        {
            try
            {
                doctor.Id = id;
                await _services.UpdateDoctor(doctor);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                await _services.DeleteDoctor(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

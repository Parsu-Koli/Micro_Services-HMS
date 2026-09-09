using AppointmentService.API.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AppointmentService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController(IDbConnection db) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentCreate model)
        {
            await db.ExecuteAsync(
                "sp_Appointment_CRUD",
                new
                {
                    Flag = "CREATE",
                    model.PatientId,
                    model.DoctorId,
                    model.AppointmentDate
                },
                commandType: CommandType.StoredProcedure
            );

            return Ok("Appointment booked");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await db.QueryAsync(
                "sp_Appointment_CRUD",
                new { Flag = "GET_ALL" },
                commandType: CommandType.StoredProcedure
            );

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await db.QueryFirstOrDefaultAsync(
                "sp_Appointment_CRUD",
                new { Flag = "GET_BY_ID", Id = id },
                commandType: CommandType.StoredProcedure
            );

            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(AppointmentUpdateStatus model)
        {
            await db.ExecuteAsync(
                "sp_Appointment_CRUD",
                new
                {
                    Flag = "UPDATE_STATUS",
                    model.Id,
                    model.Status
                },
                commandType: CommandType.StoredProcedure
            );

            return Ok("Status updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await db.ExecuteAsync(
                "sp_Appointment_CRUD",
                new { Flag = "DELETE", Id = id },
                commandType: CommandType.StoredProcedure
            );

            return Ok("Appointment deleted");
        }
    }
}

using BillingService.API.Models;
using BillingService.API.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BillingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController(BillingServices services) : ControllerBase
    {
        

        [HttpGet]
        public async Task<IActionResult> GetAllBill()
        {
            try
            {
                var result = await services.GetAllBills();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillById(int id)
        {
            try
            {
                var result = await services.GetBillById(id);
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
        public async Task<IActionResult> GenerateBill(BillCreate billCreate)
        {
            try
            {
                var result = await services.CreateBill(billCreate);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id,[FromBody]BillPaymentUpdate billPaymentUpdate)
        {
            try
            {
                billPaymentUpdate.Id = id;
                await services.UpdateBill(billPaymentUpdate);
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
        public async Task<IActionResult> DeleteBill(int id)
        {
            try
            {
                await services.DeleteBill(id);
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

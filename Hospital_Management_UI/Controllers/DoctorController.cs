using Hospital_Management_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Hospital_Management_UI.Controllers
{
    public class DoctorController : Controller
    {
        private readonly HttpClient _httpClient;

        public DoctorController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7222/"); // Ocelot
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/doctor");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to load doctors";
                return View(new List<Doctor>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var doctors = JsonConvert.DeserializeObject<List<Doctor>>(json);

            return View(doctors);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
                return View(doctor);

            var json = JsonConvert.SerializeObject(doctor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/doctor", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = "Doctor creation failed";
            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/doctor/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var doctor = JsonConvert.DeserializeObject<Doctor>(json);

            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
                return View(doctor);

            var json = JsonConvert.SerializeObject(doctor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/doctor/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = "Update failed";
            return View(doctor);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"/doctor/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var doctor = JsonConvert.DeserializeObject<Doctor>(json);

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"/doctor/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }

    }
}

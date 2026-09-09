using DoctorService.API.Models;

namespace DoctorService.API.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor?> GetDoctorById(int id);
        Task CreateDoctor(Doctor doctor);
        Task<bool> UpdateDoctor(Doctor doctor);
        Task<bool> DeleteDoctor(int id);
    }
}

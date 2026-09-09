using AppointmentService.API.Models;

namespace AppointmentService.API.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<AppointmentCreate>> GetAllAppointments();
        Task<int> CreateAppointment(AppointmentCreate createappointment);
        Task<bool> UpdateAppointment(AppointmentUpdateStatus appointment);
        //Task<bool> DeleteAppointment(int id);
    }
}

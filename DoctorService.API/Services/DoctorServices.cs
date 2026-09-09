using DoctorService.API.Models;
using DoctorService.API.Repositories.Interfaces;
using System.Data;

namespace DoctorService.API.Services
{
    public class DoctorServices(IDoctorRepository repository)
    {
        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            try
            {
                return await repository.GetAllDoctors();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid Doctor Id");

                var result = await repository.GetDoctorById(id);

                return result ?? throw new KeyNotFoundException("Doctor Not Fount");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateDoctor(Doctor doctor)
        {
            try
            {
                ValidateDoctor(doctor);
                await repository.CreateDoctor(doctor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateDoctor(Doctor doctor)
        {
            try
            {
                if (doctor.Id <= 0)
                    throw new ArgumentException("Invalid Doctor Id");
                ValidateDoctor(doctor);

                var result = await repository.UpdateDoctor(doctor);

                if (!result)
                    throw new KeyNotFoundException("Doctor Not Found");
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> DeleteDoctor(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid Doctor Id");

                var result = await repository.DeleteDoctor(id);

                if (!result)
                    throw new KeyNotFoundException("Doctor Not Found");

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private static void ValidateDoctor(Doctor doctor) {
            ArgumentNullException.ThrowIfNull(doctor);

            if (string.IsNullOrWhiteSpace(doctor.FullName))
                throw new ArgumentException("Doctor Name Is Required");

        }
    }
}

using PatientService.API.Models;
using PatientService.API.Repositories.Interfaces;

namespace PatientService.API.Services
{
    public class PatientServices
    {
        private readonly IPatientRepository _patientRepository;

        public PatientServices(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            try
            {
                return await _patientRepository.GetAllPatientsAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid patient id");

                var patient = await _patientRepository.GetPatientByIdAsync(id);

                return patient ?? throw new KeyNotFoundException("Patient not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreatePatientAsync(Patient patient)
        {
            try
            {
                ValidatePatient(patient);

                return await _patientRepository.CreatePatientAsync(patient);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            try
            {
                if (patient.Id <= 0)
                    throw new ArgumentException("Invalid patient id");

                ValidatePatient(patient);

                var updated = await _patientRepository.UpdatePatientAsync(patient);

                if (!updated)
                    throw new KeyNotFoundException("Patient not found");

                return updated;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid patient id");

                var deleted = await _patientRepository.DeletePatientAsync(id);

                if (!deleted)
                    throw new KeyNotFoundException("Patient not found");

                return deleted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ValidatePatient(Patient patient)
        {
            ArgumentNullException.ThrowIfNull(patient);

            if (string.IsNullOrWhiteSpace(patient.FullName))
                throw new ArgumentException("Patient name is required");

            if (patient.Age <= 0 || patient.Age > 120)
                throw new ArgumentException("Invalid patient age");

            if (string.IsNullOrWhiteSpace(patient.Gender))
                throw new ArgumentException("Gender is required");
        }
    }
}

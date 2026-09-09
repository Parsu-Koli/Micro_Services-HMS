using Dapper;
using PatientService.API.Models;
using PatientService.API.Repositories.Interfaces;
using System.Data;

namespace PatientService.API.Repositories.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IDbConnection _connection;

        public PatientRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GET_ALL");

                return await _connection.QueryAsync<Patient>(
                    "sp_Patient_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GET_BY_ID");
                parameters.Add("@Id", id);

                return await _connection.QueryFirstOrDefaultAsync<Patient>(
                    "sp_Patient_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
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
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "CREATE");
                parameters.Add("@Name", patient.FullName);
                parameters.Add("@Age", patient.Age);
                parameters.Add("@Gender", patient.Gender);
                parameters.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync(
                    "sp_Patient_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return parameters.Get<int>("@NewId");
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
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "UPDATE");
                parameters.Add("@Id", patient.Id);
                parameters.Add("@Name", patient.FullName);
                parameters.Add("@Age", patient.Age);
                parameters.Add("@Gender", patient.Gender);

                var rows = await _connection.ExecuteAsync(
                    "sp_Patient_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return rows > 0;
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
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "DELETE");
                parameters.Add("@Id", id);

                var rows = await _connection.ExecuteAsync(
                    "sp_Patient_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return rows > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

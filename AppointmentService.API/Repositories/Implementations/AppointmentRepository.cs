using AppointmentService.API.Models;
using AppointmentService.API.Repositories.Interfaces;
using Dapper;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace AppointmentService.API.Repositories.Implementations
{
    public class AppointmentRepository(IDbConnection dbConnection) : IAppointmentRepository
    {
        public async Task<IEnumerable<AppointmentCreate>> GetAllAppointments()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GET_ALL");

                return await dbConnection.QueryAsync<AppointmentCreate>(
                    "sp_Appointment_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }catch(Exception)
            {
                throw;
            }

        }

        public async Task<int> CreateAppointment(AppointmentCreate appointmentCreate)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "CREATE");
                parameters.Add("@PatientId", appointmentCreate.PatientId);
                parameters.Add("@DoctorId", appointmentCreate.DoctorId);
                parameters.Add("@AppointmentDate", appointmentCreate.AppointmentDate);
                parameters.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await dbConnection.ExecuteAsync(
                    "sp_Appointment_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("@NewId");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAppointment(AppointmentUpdateStatus appointment)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "UPDATE_STATUS");
                parameters.Add("@Status", appointment.Status);

                var row = await dbConnection.ExecuteAsync(
                         "sp_Appointment_CRUD",
                         parameters,
                         commandType: CommandType.StoredProcedure

                     );

                return row > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<bool> DeleteAppointment(int id)
        //{

        //}
    }
}
 
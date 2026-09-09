using BillingService.API.Models;
using BillingService.API.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace BillingService.API.Repositories.Implementation
{
    public class BillingRepository(IDbConnection connection) : IBillingRepository
    {
        public async Task<IEnumerable<BillCreate>> GetAllBills()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GET_ALL");

                return await connection.QueryAsync<BillCreate>(
                    "sp_Billing_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BillCreate?> GetBillById(int id)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Flag", "GET_BY_PATIENT");
                parameter.Add("@PatientId", id);

                return await connection.QueryFirstOrDefaultAsync<BillCreate>(
                    "sp_Billing_CRUD",
                    parameter,
                    commandType:CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CreateBill(BillCreate billCreate)
        {
            try
            {
                var parameters= new DynamicParameters();
                parameters.Add("@Flag", "CREATE");
                parameters.Add("@AppointmentId", billCreate.AppointmentId);
                parameters.Add("@PatientId", billCreate.PatientId);
                parameters.Add("@DoctorId",billCreate.DoctorId);
                parameters.Add("@Amount",billCreate.Amount);
                parameters.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("sp_Billing_CRUD",
                    parameters,
                    commandType:CommandType.StoredProcedure);

                return parameters.Get<int>("@NewId");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBill(BillPaymentUpdate bill)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Flag", "UPDATE_PAYMENT");
            parameters.Add("@PaymentStatus", bill.PaymentStatus);

            var row = await connection.ExecuteAsync(
                "sp_Billing_CRUD",
                parameters,
                commandType: CommandType.StoredProcedure);

            return row > 0;

        }

        public async Task<bool> DeleteBill(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "DELETE");
                parameters.Add("@Id", id);

                var result = await connection.ExecuteAsync(
                    "sp_Billing_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Dapper;
using DoctorService.API.Models;
using DoctorService.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace DoctorService.API.Repositories.Implementation
{

    public class DoctorRepository(IDbConnection dbConnection) : IDoctorRepository
    {
        

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GET_ALL");

                return await dbConnection.QueryAsync<Doctor>("sp_Doctor_CRUD", parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Doctor?> GetDoctorById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GET_BY_ID");
                parameters.Add("@Id", id);

                return await dbConnection.QueryFirstOrDefaultAsync<Doctor>(
                    "sp_Doctor_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateDoctor(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            var parameters = new DynamicParameters();
            parameters.Add("@Flag", "CREATE");
            parameters.Add("@UserName", doctor.UserName);
            parameters.Add("@FullName", doctor.FullName);
            parameters.Add("@Specialization", doctor.Specialization);
            parameters.Add("@Department", doctor.Department);
            parameters.Add("@Email", doctor.Email);
            parameters.Add("@Phone", doctor.Phone);
            parameters.Add("@Password", doctor.Password);

            await dbConnection.ExecuteAsync(
                "sp_Doctor_CRUD",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }


        public async Task<bool> UpdateDoctor(Doctor doctor)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Update");
                parameters.Add("@UserName", doctor.UserName);
                parameters.Add("@FullName", doctor.FullName);
                parameters.Add("@Specialization", doctor.Specialization);
                parameters.Add("@Department", doctor.Department);
                parameters.Add("@Email", doctor.Email);
                parameters.Add("@Phone", doctor.Phone);

                var row = await dbConnection.ExecuteAsync(
                    "sp_Doctor_CRUD",
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

        public async Task<bool> DeleteDoctor(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Delete");
                parameters.Add("@Id", id);

                var resut = await dbConnection.ExecuteAsync(
                    "sp_Doctor_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                    );
                return resut > 0;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

    

}

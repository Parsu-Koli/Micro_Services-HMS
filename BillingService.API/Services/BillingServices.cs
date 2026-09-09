using BillingService.API.Models;
using BillingService.API.Repositories.Interfaces;

namespace BillingService.API.Services
{
    public class BillingServices(IBillingRepository billingRepository)
    {
        public async Task<IEnumerable<BillCreate>> GetAllBills()
        {
            try
            {
                return await billingRepository.GetAllBills();
            }
            catch(Exception) {
                throw;
            }
        }

        public async Task<BillCreate> GetBillById(int id)
        {
            try
            {
                if (id < 0)
                    throw new ArgumentException("Invalid Bill Id");

                var result = await billingRepository.GetBillById(id);

                return result ?? throw new KeyNotFoundException("Bill Not Found");
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
                return await billingRepository.CreateBill(billCreate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBill(BillPaymentUpdate billPaymentUpdate)
        {
            try
            {
                if (billPaymentUpdate.Id <= 0)
                    throw new ArgumentException("Invalid Bill Id");

                var result = await billingRepository.UpdateBill(billPaymentUpdate);

                if (!result)
                    throw new KeyNotFoundException("Bill Not Found");

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteBill(int id)
        {
            try
            {
                if (id < 0)
                    throw new ArgumentException("Invalid Bill Id Please enter Valid Id");

                var result = await billingRepository.DeleteBill(id);

                if (!result)
                    throw new KeyNotFoundException("Bill Not Found");

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        
        }
    }
}

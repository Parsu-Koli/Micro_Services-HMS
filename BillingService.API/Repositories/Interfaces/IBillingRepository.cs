using BillingService.API.Models;

namespace BillingService.API.Repositories.Interfaces
{
    public interface IBillingRepository
    {
        Task<IEnumerable<BillCreate>> GetAllBills();
        Task<BillCreate?> GetBillById(int id);
        Task<int> CreateBill(BillCreate bill);
        Task<bool> UpdateBill(BillPaymentUpdate bill);
        Task<bool> DeleteBill(int id);
    }
}

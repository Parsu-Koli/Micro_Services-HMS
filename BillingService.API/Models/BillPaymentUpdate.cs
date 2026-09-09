namespace BillingService.API.Models
{
    public class BillPaymentUpdate
    {
        public int Id { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}

namespace BillingService.API.Models
{
    public class BillCreate
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public decimal Amount { get; set; }
    }
}

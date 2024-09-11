using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.Entities;
public class BillingService
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public BillingType BillingType { get; set; }
    public PaymentType PaymentType { get; set; }
    public string Observation { get; set; } = string.Empty;
}

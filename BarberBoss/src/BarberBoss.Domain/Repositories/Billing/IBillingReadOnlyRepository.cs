using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billing;
public interface IBillingReadOnlyRepository
{
    Task<List<BillingService>> GetAll();

    Task<BillingService?> GetById(long id);

    Task<List<BillingService>> FilterByMonth(DateTime startDate, DateTime endDate);
}

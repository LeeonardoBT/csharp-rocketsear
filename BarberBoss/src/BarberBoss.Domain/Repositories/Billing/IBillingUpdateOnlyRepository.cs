using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billing;
public interface IBillingUpdateOnlyRepository
{
    Task<BillingService?> GetById(long id);
    void Update(BillingService billing);
}

using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Billing;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
internal class BillingRepository : IBillingWriteOnlyRepository, IBillingUpdateOnlyRepository, IBillingReadOnlyRepository
{
    private readonly BarberBossDbContext _dbContext;

    public BillingRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(BillingService billing)
    {
        await _dbContext.BillingService.AddAsync(billing);
    }

    async Task<BillingService?> IBillingUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.BillingService.FirstOrDefaultAsync(bil => bil.Id == id);
    }

    public void Update(BillingService billing)
    {
        _dbContext.BillingService.Update(billing);
    }

    public async Task<List<BillingService>> GetAll()
    {
        return await _dbContext.BillingService.AsNoTracking().ToListAsync();
    }

    Task<BillingService?> IBillingReadOnlyRepository.GetById(long id)
    {
        return _dbContext.BillingService.AsNoTracking().FirstOrDefaultAsync(bil => bil.Id == id);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.BillingService.FirstOrDefaultAsync(bil => bil.Id == id);

        if(result == null)
        {
            return false;
        }

        _dbContext.BillingService.Remove(result);

        return true;
    }

    public async Task<List<BillingService>> FilterByMonth(DateTime startDate, DateTime endDate)
    {
        return await _dbContext.BillingService.
            AsNoTracking().
            Where(bil => bil.Date >= startDate && bil.Date <= endDate).
            OrderBy(bil => bil.Date).
            ToListAsync();
    }
}

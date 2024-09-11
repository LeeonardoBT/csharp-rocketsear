using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository : IExpenseWriteOnlyRepository, IExpenseReadOnlyRepository, IExpenseUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;

    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);

        if(result is null) 
        { 
            return false;
        }

        _dbContext.Expenses.Remove(result);

        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpenseReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(exp => exp.Id == id);
    }

    async Task<Expense?> IExpenseUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(exp => exp.Id == id);
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(DateTime date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
        var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: lastDay, hour: 23, minute: 59, second: 59);

        return await _dbContext.Expenses.
            AsNoTracking().
            Where(expense => expense.Date >= startDate && expense.Date <= endDate).
            OrderBy(expense => expense.Date).
            ToListAsync();
    }
}

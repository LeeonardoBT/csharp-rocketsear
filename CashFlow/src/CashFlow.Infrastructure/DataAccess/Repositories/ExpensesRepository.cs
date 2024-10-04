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

    public async Task Delete(long id)
    {
        var result = await _dbContext.Expenses.FindAsync(id);

        _dbContext.Expenses.Remove(result!);
    }

    public async Task<List<Expense>> GetAll(User user)
    {
        return await _dbContext.Expenses
            .AsNoTracking()
            .Where(exp => exp.UserId == user.Id)
            .ToListAsync();
    }

    async Task<Expense?> IExpenseReadOnlyRepository.GetById(User user, long id)
    {
        return await _dbContext.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(exp => exp.Id == id && exp.UserId == user.Id);
    }

    async Task<Expense?> IExpenseUpdateOnlyRepository.GetById(User user, long id)
    {
        return await _dbContext.Expenses
            .FirstOrDefaultAsync(exp => exp.Id == id && exp.UserId == user.Id);
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(User user, DateTime date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
        var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: lastDay, hour: 23, minute: 59, second: 59);

        return await _dbContext.Expenses.
            AsNoTracking().
            Where(expense => expense.UserId == user.Id && expense.Date >= startDate && expense.Date <= endDate).
            OrderBy(expense => expense.Date).
            ToListAsync();
    }
}

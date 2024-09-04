namespace CashFlow.Application.UseCase.Expenses.Reports.Excel;
public interface IGenerateExpensesReportExcelUseCase
{
    Task<byte[]> Execute(DateTime month);
}

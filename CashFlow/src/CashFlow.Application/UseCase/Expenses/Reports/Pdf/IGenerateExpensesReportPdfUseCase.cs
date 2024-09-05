namespace CashFlow.Application.UseCase.Expenses.Reports.PDF;
public interface IGenerateExpensesReportPdfUseCase
{
    Task<byte[]> Execute(DateTime month);
}

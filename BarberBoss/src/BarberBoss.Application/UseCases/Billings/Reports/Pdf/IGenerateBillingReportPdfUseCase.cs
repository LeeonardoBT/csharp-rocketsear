namespace BarberBoss.Application.UseCases.Billings.Reports.Pdf;
public interface IGenerateBillingReportPdfUseCase
{
    Task<byte[]> Execute(DateTime month);
}

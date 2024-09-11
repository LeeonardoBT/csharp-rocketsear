using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Billing;
using BarberBoss.Domain.Utils;
using ClosedXML.Excel;

namespace BarberBoss.Application.UseCases.Billings.Reports.Excel;
public class GenerateBillingReportExcelUseCase : IGenerateBillingReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "$";
    private readonly IBillingReadOnlyRepository _repository;

    public GenerateBillingReportExcelUseCase(IBillingReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateTime month)
    {
        var startDate = ReturnReportDates.ReturnStartDate(month);
        var endDate = ReturnReportDates.ReturnEndDate(month);

        var billings = await _repository.FilterByMonth(startDate, endDate);

        if (billings.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();

        workbook.Author = "BarberBoss";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Arial";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var raw = 2;
        foreach (var bill in billings) 
        {
            worksheet.Cell($"A{raw}").Value = bill.BillingType.BillintTypeToString();
            worksheet.Cell($"B{raw}").Value = bill.Date.ToString("d");
            worksheet.Cell($"C{raw}").Value = bill.PaymentType.PaymentTypeToString();

            worksheet.Cell($"D{raw}").Value = bill.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00";

            worksheet.Cell($"E{raw}").Value = bill.Observation;

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Font.FontColor = XLColor.FromHtml("#FFFFFF");
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");
        worksheet.Cells("A1:E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}

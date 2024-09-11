
using BarberBoss.Application.UseCases.Billings.Reports.Pdf.Colors;
using BarberBoss.Application.UseCases.Billings.Reports.Pdf.Fonts;
using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Billing;
using BarberBoss.Domain.Utils;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Billings.Reports.Pdf;
public class GenerateBillingReportPdfUseCase : IGenerateBillingReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "$";
    private const int HEIGHT_ROW_BILLING_TABLE = 25;
    private readonly IBillingReadOnlyRepository _repository;

    public GenerateBillingReportPdfUseCase(IBillingReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new BillingsReportFontResolver();
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

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhoneAndName(page);

        var totalBilling = billings.Sum(bil => bil.Amount);
        CreateTotalBillingSection(page, month, totalBilling);

        foreach (var bil in billings)
        {
            var table = CreateBillingTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_BILLING_TABLE;

            AddBillingTitle(row.Cells[0], bil.BillingType.BillintTypeToString());
            AddAmountHeader(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_BILLING_TABLE;

            row.Cells[0].AddParagraph(bil.Date.ToString("D"));
            SetStyleBaseForExpenseInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 10;

            row.Cells[1].AddParagraph(bil.Date.ToString("t"));
            SetStyleBaseForExpenseInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(bil.PaymentType.PaymentTypeToString());
            SetStyleBaseForExpenseInformation(row.Cells[2]);

            AddAmountForBilling(row.Cells[3], bil.Amount);

            if (string.IsNullOrWhiteSpace(bil.Observation) == false)
            {
                var description = table.AddRow();
                description.Height = HEIGHT_ROW_BILLING_TABLE;

                description.Cells[0].AddParagraph(bil.Observation);
                description.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorHelper.DARK_GRAY };
                description.Cells[0].Shading.Color = ColorHelper.LIGHT_GREEN;
                description.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                description.Cells[0].MergeRight = 2;
                description.Cells[0].Format.LeftIndent = 10;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }

        return RenderDocument(document);
    }

    private Document CreateDocument(DateTime month)
    {
        var document = new Document();

        document.Info.Title = $"{month:Y}";
        document.Info.Author = "BarberBoss";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private void CreateHeaderWithProfilePhoneAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directory = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directory!, "Images", "Profile.png");

        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph("Gentleman, Barber Boss!");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 18 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private void CreateTotalBillingSection(Section page, DateTime month, decimal totalExpenses)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerationMessages.MONTH_BILLING, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 16 });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{totalExpenses} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateBillingTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("215").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("90").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private void AddBillingTitle(Cell cell, string title)
    {
        cell.AddParagraph(title);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.WHITE };
        cell.Shading.Color = ColorHelper.DARK_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 10;
    }

    private void AddAmountHeader(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.WHITE };
        cell.Shading.Color = ColorHelper.MIDDLE_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForExpenseInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.LIGHT_GRAY;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddAmountForBilling(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{amount} {CURRENCY_SYMBOL}");
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 15, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}

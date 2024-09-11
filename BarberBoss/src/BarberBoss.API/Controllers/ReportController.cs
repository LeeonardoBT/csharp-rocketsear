using BarberBoss.Application.UseCases.Billings.Reports.Excel;
using BarberBoss.Application.UseCases.Billings.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BarberBoss.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("Excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateBillingReportExcelUseCase useCase,
        [FromQuery] DateTime month)
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "barberboss.xlsx");

        return NoContent();
    }

    [HttpGet("Pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
        [FromServices] IGenerateBillingReportPdfUseCase useCase,
        [FromQuery] DateTime month)
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "barberboss.pdf");

        return NoContent();
    }
}

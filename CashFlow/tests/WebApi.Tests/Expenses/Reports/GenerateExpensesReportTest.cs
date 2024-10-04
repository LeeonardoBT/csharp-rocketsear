using FluentAssertions;
using System.Net;
using System.Net.Mime;

namespace WebApi.Tests.Expenses.Reports;
public class GenerateExpensesReportTest : CashFlowClassFixture
{
    private const string METHOD = "api/Report";

    private readonly string _adminToken;
    private readonly string _memberTeamToken;
    private readonly DateTime _expenseDate;

    public GenerateExpensesReportTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _adminToken = webApplicationFactory.User_Admin.GetToken();
        _memberTeamToken = webApplicationFactory.User_Team_Member.GetToken();
        _expenseDate = webApplicationFactory.Expense_Admin.GetDate();
    }

    [Fact]
    public async Task SuccessPdf()
    {
        var result = await DoGet(requestUri: $"{METHOD}/Pdf?month={_expenseDate:s}", token: _adminToken);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Pdf);
    }

    [Fact]
    public async Task SuccessExcel()
    {
        var result = await DoGet(requestUri: $"{METHOD}/Excel?month={_expenseDate:s}", token: _adminToken);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Octet);
    }

    [Fact]
    public async Task ErrorForbiddenUserNotAllowedPdf()
    {
        var result = await DoGet(requestUri: $"{METHOD}/Pdf?month={_expenseDate:s}", token: _memberTeamToken);

        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ErrorForbiddenUserNotAllowedExcel()
    {
        var result = await DoGet(requestUri: $"{METHOD}/Excel?month={_expenseDate:s}", token: _memberTeamToken);

        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}

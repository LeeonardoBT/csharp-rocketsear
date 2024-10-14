using FluentAssertions;
using System.Net;

namespace WebApi.Tests.Users.Delete;
public class DeleteProfileTest : CashFlowClassFixture
{
    private const string METHOD = "api/User";

    private readonly string _token;

    public DeleteProfileTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(METHOD, _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}

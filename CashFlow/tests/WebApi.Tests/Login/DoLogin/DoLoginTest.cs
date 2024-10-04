using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InLineData;

namespace WebApi.Tests.Login.DoLogin;
public class DoLoginTest : CashFlowClassFixture
{
    private const  string METHOD = "api/Login";

    private readonly string _email;
    private readonly string _name;
    private readonly string _password;

    public DoLoginTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _email = webApplicationFactory.User_Team_Member.GetEmail();
        _name = webApplicationFactory.User_Team_Member.GetName();
        _password = webApplicationFactory.User_Team_Member.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };

        var response = await DoPost(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStreamAsync();

        var data = await JsonDocument.ParseAsync(body);

        data.RootElement.GetProperty("name").GetString().Should().Be(_name);
        data.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [ClassData(typeof(CultureInLineDataTest))]
    public async Task ErrorInvalidLogin(string culture)
    {
        var request = RequestLoginJsonBuilder.Build();

        var response = await DoPost(requestUri: METHOD, request: request, culture: culture);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await response.Content.ReadAsStreamAsync();

        var data = await JsonDocument.ParseAsync(body);

        var errors = data.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(er => er.GetString()!.Equals(expectedMessage));
    }
}
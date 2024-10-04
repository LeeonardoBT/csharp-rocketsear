using CashFlow.Application.UseCase.Users.Register;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Users.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new PasswordValidator();
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void ErrorEmptyName(string name)
    {
        var validator = new PasswordValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY));
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void ErrorEmptyEmail(string email)
    {
        var validator = new PasswordValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY));
    }

    [Theory]
    [InlineData("teste.com")]
    [InlineData("teste@")]
    [InlineData("@gmail.com")]
    public void ErrorInvalidEmail(string email)
    {
        var validator = new PasswordValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
    }

    [Fact]
    public void ErrorEmptyPassword()
    {
        var validator = new PasswordValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));
    }
}

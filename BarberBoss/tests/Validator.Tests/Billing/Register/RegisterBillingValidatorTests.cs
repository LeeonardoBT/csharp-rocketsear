using BarberBoss.Application.UseCases.Billings;
using BarberBoss.Communication.Enums;
using BarberBoss.Exception;
using Bogus.DataSets;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.Billing.Register;
public class RegisterBillingValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new BillingValidator();
        var request = RequestRegisterBillingJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();   
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorNameEmpty(string name)
    {
        var validator = new BillingValidator();
        var request = RequestRegisterBillingJsonBuilder.Build();
        request.ClientName = name;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.CLIENT_NAME_EMPTY));
    }

    [Fact]
    public void ErrorFutureDate()
    {
        var validator = new BillingValidator();
        var request = RequestRegisterBillingJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.BILLING_DATE_FUTURE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.5)]
    [InlineData(-1)]
    public void ErrorAmountInvalid(decimal amount)
    {
        var validator = new BillingValidator();
        var request = RequestRegisterBillingJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_ZERO));
    }

    [Fact]
    public void ErrorBillingTypeInvalid()
    {
        var validator = new BillingValidator();
        var request = RequestRegisterBillingJsonBuilder.Build();
        request.BillingType = (BillingType)100;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.BILLING_TYPE_INVALID));
    }

    [Fact]
    public void ErrorPaymentTypeInvalid()
    {
        var validator = new BillingValidator();
        var request = RequestRegisterBillingJsonBuilder.Build();
        request.PaymentType = (PaymentType)100;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }
}

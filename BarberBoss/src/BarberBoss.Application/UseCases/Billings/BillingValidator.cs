using BarberBoss.Communication.Requests;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Billings;
public class BillingValidator : AbstractValidator<RequestBillingJson>
{
    public BillingValidator() 
    {
        RuleFor(billing => billing.ClientName).NotEmpty().WithMessage(ResourceErrorMessages.CLIENT_NAME_EMPTY);
        RuleFor(billing => billing.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_ZERO);
        RuleFor(billing => billing.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.BILLING_DATE_FUTURE);
        RuleFor(billing => billing.BillingType).IsInEnum().WithMessage(ResourceErrorMessages.BILLING_TYPE_INVALID);
        RuleFor(billing => billing.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }
}

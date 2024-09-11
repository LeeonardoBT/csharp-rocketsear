using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestRegisterBillingJsonBuilder
{
    public static RequestBillingJson Build()
    {
        return new Faker<RequestBillingJson>()
            .RuleFor(r => r.Date, f => f.Date.Past())
            .RuleFor(r => r.ClientName, f => f.Name.FullName())
            .RuleFor(r => r.Amount, f => f.Random.Decimal(min: 1, max: 500))
            .RuleFor(r => r.BillingType, f => f.PickRandom<BillingType>())
            .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentType>());
    }
}

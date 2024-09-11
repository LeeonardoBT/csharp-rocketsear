using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;

namespace BarberBoss.Domain.Extensions;
public static class BillingTypeExtension
{
    public static string BillintTypeToString(this BillingType billingType)
    {
        return billingType switch
        {
            BillingType.Haircut => ResourceReportGenerationMessages.HAIRCUT,
            BillingType.BeardTrim => ResourceReportGenerationMessages.BEARD_TRIM,
            BillingType.HairAndBeardCombo => ResourceReportGenerationMessages.HAIR_BEARD_COMBO,
            BillingType.ProductSale => ResourceReportGenerationMessages.PRODUCT_SALE,
            BillingType.MonthlySubscription => ResourceReportGenerationMessages.MONTHLY_SUBSCRIPTION,
            BillingType.WeeklySubscription => ResourceReportGenerationMessages.WEEKLY_SUBSCRIPTION,
            _ => string.Empty
        };
    }
}

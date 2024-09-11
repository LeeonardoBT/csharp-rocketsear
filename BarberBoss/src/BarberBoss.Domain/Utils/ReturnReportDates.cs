using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarberBoss.Domain.Utils;
public class ReturnReportDates
{
    public static DateTime ReturnStartDate(DateTime month)
    {
        return new DateTime(year: month.Year, month: month.Month, day: 1).Date;
    }

    public static DateTime ReturnEndDate(DateTime month)
    {
        var lastDay = DateTime.DaysInMonth(month.Year, month.Month);
        return new DateTime(year: month.Year, month: month.Month, day: lastDay, hour: 23, minute: 59, second: 59);
    }
}

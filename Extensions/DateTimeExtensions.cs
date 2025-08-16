namespace ProvaPub.Extensions
{

    public static class DateTimeExtensions
    {
        public static DateTime ToBrasiliaTime(this DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);
        }
    }
}

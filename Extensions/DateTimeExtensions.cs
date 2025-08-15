namespace ProvaPub.Extensions
{

    public static class DateTimeExtensions
    {
        public static DateTime ToBrasiliaTime(this DateTime utcDateTime)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("A data fornecida não está em UTC.");
            }

            TimeZoneInfo timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timezoneInfo);
        }
    }
}

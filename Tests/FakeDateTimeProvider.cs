using ProvaPub.Services.Interfaces;

namespace ProvaPub.Tests
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow { get; set; }
    }
}


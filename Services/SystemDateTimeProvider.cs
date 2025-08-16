﻿using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

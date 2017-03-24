using System;

using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Utils
{
    public class DateProvider : IDateProvider
    {
        public DateTime GetDate()
        {
            return DateTime.UtcNow;
        }
    }
}

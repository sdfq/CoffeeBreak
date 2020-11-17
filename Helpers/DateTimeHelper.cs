using System;
using System.Globalization;

namespace CoffeeBreak.Helpers
{
    /// <summary>
    /// Helper for work with date and time
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Current zone Izhevsk: Samara Time (SAMT) - UTC +04:00
        /// </summary>
        private const double currentTimezone = 4;

        /// <summary>
        /// Get number of week for today
        /// TODO maybe correct CalendarWeekRule in next Year
        /// </sumcalmary>
        /// <returns></returns>
        public static int GetNumberOfWeek()
        {
            var cal = new GregorianCalendar();
            return cal.GetWeekOfYear(Today(), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Get date and time of today corrected by current timezone
        /// </summary>
        /// <returns></returns>
        public static DateTime Today()
        {
            var today = DateTime.UtcNow;
            return today.AddHours(currentTimezone);
        }

        public static string GetStartAndEndOfWeek()
        {
            var today = Today().DayOfWeek;
            var sunday = DayOfWeek.Sunday + 7;
            var days = sunday - today;
            var end = Today().AddDays(days);
            var start = end.AddDays(-6);

            return $"Прайс дествителен с {start} по {end}";
        }
        
    }
}

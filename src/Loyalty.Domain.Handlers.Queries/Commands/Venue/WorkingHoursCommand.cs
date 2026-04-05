using System;

namespace Loyalty.Domain.Handlers.Queries.Commands.Venue
{
    public class WorkingHoursCommand
    {
        public DayOfWeek Day { get; set; }

        public int? From { get; set; }

        public int? To { get; set; }
    }
}

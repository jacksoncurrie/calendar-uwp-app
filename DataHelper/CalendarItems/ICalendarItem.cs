using System;

namespace DataHelper.CalendarItems
{
    public interface ICalendarItem
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        string ItemName { get; set; }
    }
}

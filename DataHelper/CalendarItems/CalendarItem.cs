using System;

namespace DataHelper.CalendarItems
{
    public class CalendarItem : ICalendarItem
    {
        public CalendarItem(DateTime startDateTime, DateTime endDateTime, string itemName)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            ItemName = itemName;
        }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ItemName { get; set; }
    }
}

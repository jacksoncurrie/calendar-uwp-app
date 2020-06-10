using DataHelper.CalendarItems;
using System;

namespace DataHelper.TaskItems
{
    public interface ITaskItem
    {
        string TaskName { get; set; }

        ICalendarItem ConvertToCalendarItem(DateTime startDateTime, DateTime endDateTime);
    }
}

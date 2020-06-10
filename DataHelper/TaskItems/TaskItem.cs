using DataHelper.CalendarItems;
using System;

namespace DataHelper.TaskItems
{
    public class TaskItem : ITaskItem
    {
        public TaskItem(string taskName)
        {
            TaskName = taskName;
        }

        public string TaskName { get; set; }

        public ICalendarItem ConvertToCalendarItem(DateTime startDateTime, DateTime endDateTime) =>
            new CalendarItem(startDateTime, endDateTime, TaskName);
    }
}

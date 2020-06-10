using DataHelper.CalendarItems;
using DataHelper.TaskItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;
using Windows.UI;

namespace DataHelper
{
    public static class DataItemHelper
    {
        public static ObservableCollection<ITaskItem> TaskItems { get; set; }
        public static ObservableCollection<ICalendarItem> CalendarItems { get; set; }
        public static ObservableCollection<ICalendarItem> CurrentDayItems { get; set; }
        public static Color DensityColor { get; set; } = Colors.Blue;
        public static int DensityColorIndex { get; set; } = 0;

        public static void MoveTaskToCalendar(ITaskItem task, DateTime startDateTime, DateTime endDateTime)
        {
            // Create calendar item and add to list
            var calendarItem = task.ConvertToCalendarItem(startDateTime, endDateTime);
            CalendarItems.Add(calendarItem);

            // Remove the left over task from the task list
            TaskItems.Remove(task);
        }
    }
}

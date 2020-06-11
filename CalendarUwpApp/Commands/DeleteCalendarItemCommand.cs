using DataHelper;
using DataHelper.CalendarItems;
using System;
using System.Windows.Input;

namespace CalendarUwpApp.Commands
{
    class DeleteCalendarItemCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is ICalendarItem;
        }

        public void Execute(object parameter)
        {
            var calendarItem = parameter as ICalendarItem;
            DataItemHelper.CurrentDayItems.Remove(calendarItem);
            DataItemHelper.CalendarItems.Remove(calendarItem);
        }
    }
}

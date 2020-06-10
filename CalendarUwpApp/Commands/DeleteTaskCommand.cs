using DataHelper;
using DataHelper.TaskItems;
using System;
using System.Windows.Input;

namespace CalendarUwpApp
{
    class DeleteTaskCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is ITaskItem;
        }

        public void Execute(object parameter)
        {
            var task = parameter as ITaskItem;
            DataItemHelper.TaskItems.Remove(task);
        }
    }
}

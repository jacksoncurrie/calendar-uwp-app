using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DataHelper;
using DataHelper.TaskItems;
using System;
using Windows.UI.Xaml.Input;
using System.Threading.Tasks;
using Windows.UI.Popups;
using CalendarUwpApp.Commands;

namespace CalendarUwpApp
{
    public sealed partial class TaskList : Page
    {
        public TaskList()
        {
            InitializeComponent();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var taskNameInput = new TextBox { Height = 30 };
            var dialog = new ContentDialog
            {
                Title = "New Task",
                Content = taskNameInput,
                PrimaryButtonText = "Add Task",
                CloseButtonText = "Close"
            };

            // Event for when enter key is pressed
            taskNameInput.KeyDown += async delegate (object keyDownSender, KeyRoutedEventArgs keyDownE)
            {
                if (keyDownE.Key == Windows.System.VirtualKey.Enter)
                {
                    await CheckAndAddTask(taskNameInput.Text);
                    dialog.Hide();
                }
            };

            try
            {
                // Result when "Add Task" is pressed
                var dialogResult = await dialog.ShowAsync();
                if (dialogResult == ContentDialogResult.Primary)
                {
                    await CheckAndAddTask(taskNameInput.Text);
                }
            }
            catch
            {
                // Dialog already open
                return;
            }
        }

        private async Task CheckAndAddTask(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
            {
                var message = new MessageDialog("No value entered, task not added.", "Invalid Input");
                await message.ShowAsync();
            } 
            else
                DataItemHelper.TaskItems.Add(new TaskItem(taskName));
        }

        private async void TasksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedItem = e.ClickedItem as ITaskItem;

            var dialog = new ContentDialog
            {
                Title = selectedItem.TaskName,
                PrimaryButtonText = "Add To Calendar",
                SecondaryButtonText = "Delete Task",
                SecondaryButtonCommand = new DeleteTaskCommand(),
                SecondaryButtonCommandParameter = selectedItem,
                CloseButtonText = "Close"
            };

            try
            {
                var dialogResult = await dialog.ShowAsync();
                if (dialogResult == ContentDialogResult.Primary)
                    await AddTaskToCalendar(selectedItem);
            }
            catch
            {
                // Dialog already open
                return;
            }
        }

        private async Task AddTaskToCalendar(ITaskItem task)
        {
            var addToCalendarForm = new AddToCalendarForm(false);
            var dialog = new ContentDialog
            {
                Title = task.TaskName,
                Content = addToCalendarForm,
                PrimaryButtonText = "Submit",
                CloseButtonText = "Close"
            };
            try
            {
                var dialogResult = await dialog.ShowAsync();
                if (dialogResult == ContentDialogResult.Primary)
                {
                    if (addToCalendarForm.InputsValid)
                    {
                        var startDateTime = addToCalendarForm.StartDateTime;
                        var endDateTime = addToCalendarForm.EndDateTime;
                        DataItemHelper.MoveTaskToCalendar(task, startDateTime, endDateTime);
                    }
                    else
                    {
                        var message = new MessageDialog("Not all values were completed, task not added to calendar.", "Invalid Inputs");
                        await message.ShowAsync();
                    }
                }
            }
            catch
            {
                // Dialog already open
                return;
            }
        }

        private FrameworkElement OriginalSource { get; set; }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Run as command is already set up to handle this
            var deleteCommand = new DeleteTaskCommand();
            deleteCommand.Execute(OriginalSource.DataContext);
        }

        private async void AddToCalendar_Click(object sender, RoutedEventArgs e)
        {
            await AddTaskToCalendar(OriginalSource.DataContext as ITaskItem);
        }

        private void TasksList_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            OriginalSource = e.OriginalSource as FrameworkElement;
        }
    }
}

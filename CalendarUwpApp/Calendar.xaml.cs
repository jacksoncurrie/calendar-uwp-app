using DataHelper;
using DataHelper.CalendarItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace CalendarUwpApp
{
    public sealed partial class Calendar : Page
    {
        public Calendar()
        {
            InitializeComponent();
        }

        private async void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            var daySelected = args.AddedDates;
            if (daySelected.Count < 1)
                daySelected = args.RemovedDates;

            var dateSelected = daySelected.First().Date;

            var currentDayItems = new CurrentDaysItems(dateSelected);
            var dayItemDialog = new ContentDialog
            {
                Title = dateSelected.ToLongDateString(),
                Content = currentDayItems,
                PrimaryButtonText = "Add New Item",
                CloseButtonText = "Close"
            };

            currentDayItems.OpenCurrentDayItem += async delegate (object dayItemSender, EventArgs dayItemArgs)
            {
                var calendarItem = dayItemSender as ICalendarItem;
                dayItemDialog.Hide();
                await AddNewCalendarItem(calendarItem.StartDateTime, calendarItem.EndDateTime, calendarItem.ItemName, false, calendarItem);
            };

            var currentDayResult = await dayItemDialog.ShowAsync();

            if (currentDayResult != ContentDialogResult.Primary)
            {
                // Used to reload the page
                if (currentDayItems.RequiresRefresh)
                {
                    Frame.Navigate(typeof(Calendar));
                    Frame.GoBack();
                }
                return;
            }
            await AddNewCalendarItem(dateSelected, dateSelected);
        }

        private async Task AddNewCalendarItem(DateTime startDate, DateTime endDate, string itemName = null, bool isNew = true, ICalendarItem currentItem = null)
        {
            var addToCalendarForm = new AddToCalendarForm(true, startDate, endDate, itemName);
            var dialog = new ContentDialog
            {
                Title = isNew ? "Add Calendar Item" : "Update Calendar Item",
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
                        // Remove before re-adding on update
                        if (!isNew)
                            DataItemHelper.CalendarItems.Remove(currentItem);

                        var calendarItem = new CalendarItem(addToCalendarForm.StartDateTime, addToCalendarForm.EndDateTime, addToCalendarForm.ItemName);
                        DataItemHelper.CalendarItems.Add(calendarItem);

                        // Used to reload the page
                        Frame.Navigate(typeof(Calendar));
                        Frame.GoBack();
                    }
                    else
                    {
                        var message = new MessageDialog("Not all values were completed, task not added to calendar.", "Invalid Inputs");
                        await message.ShowAsync();
                    }
                } 
                else
                {
                    // Used to reload the page
                    Frame.Navigate(typeof(Calendar));
                    Frame.GoBack();
                }
            }
            catch
            {
                // Dialog already open
                return;
            }
        }

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            // Add all the day items loaded
            var currentItems = DataItemHelper.CalendarItems.Where(i =>
                 args.Item.Date.Date >= i.StartDateTime.Date && args.Item.Date.Date <= i.EndDateTime.Date);

            var densityColors = new List<Color>();
            foreach(var item in currentItems)
                densityColors.Add(DataItemHelper.DensityColor);

            args.Item.SetDensityColors(densityColors);
        }
    }
}

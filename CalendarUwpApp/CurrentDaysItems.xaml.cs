using CalendarUwpApp.Commands;
using DataHelper;
using DataHelper.CalendarItems;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace CalendarUwpApp
{
    public sealed partial class CurrentDaysItems : UserControl
    {
        public event EventHandler OpenCurrentDayItem;

        public CurrentDaysItems(DateTime selectedDate)
        {
            InitializeComponent();

            // Get any event which date includes the selected date
            var currentItems = DataItemHelper.CalendarItems.Where(i =>
                 selectedDate >= i.StartDateTime.Date && selectedDate <= i.EndDateTime.Date);

            DataItemHelper.CurrentDayItems = new ObservableCollection<ICalendarItem>();

            foreach (var calendarItem in currentItems)
                DataItemHelper.CurrentDayItems.Add(calendarItem);
        }

        public bool RequiresRefresh { get; set; } = false;

        private FrameworkElement OriginalSource { get; set; }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var deleteCommand = new DeleteCalendarItemCommand();
            deleteCommand.Execute(OriginalSource.DataContext);
            RequiresRefresh = true;
        }

        private void CurrentDayList_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            // Store so we have refrence when an action is taken
            OriginalSource = e.OriginalSource as FrameworkElement;
        }

        private void CurrentDayList_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenCurrentDayItem.Invoke(e.ClickedItem, new EventArgs());
        }
    }
}

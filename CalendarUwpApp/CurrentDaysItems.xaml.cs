using DataHelper;
using DataHelper.CalendarItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CalendarUwpApp
{
    public sealed partial class CurrentDaysItems : UserControl
    {
        public event EventHandler OpenCurrentDayItem;

        public CurrentDaysItems(DateTime selectedDate)
        {
            InitializeComponent();

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
            DataItemHelper.CurrentDayItems.Remove(OriginalSource.DataContext as ICalendarItem);
            DataItemHelper.CalendarItems.Remove(OriginalSource.DataContext as ICalendarItem);
            RequiresRefresh = true;
        }

        private void CurrentDayList_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            OriginalSource = e.OriginalSource as FrameworkElement;
        }

        private void CurrentDayList_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenCurrentDayItem.Invoke(e.ClickedItem, new EventArgs());
        }
    }
}

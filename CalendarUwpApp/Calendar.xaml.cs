using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CalendarUwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Calendar : Page
    {
        public Calendar()
        {
            InitializeComponent();
            CalendarViewDayItems = new List<CalendarViewDayItem>();
        }

        private List<CalendarViewDayItem> CalendarViewDayItems { get; }

        private async void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            var cd = new ContentDialog
            {
                Content = "Adding Colors to Date",
                PrimaryButtonText = "Close"
            };

            foreach (var item in sender.SelectedDates)
            {
                var currentDayItem = CalendarViewDayItems.SingleOrDefault(i => i.Date == item);
                if (currentDayItem != null)
                {
                    await cd.ShowAsync();
                    var densityColors = new List<Color> { Colors.Green, Colors.Blue };
                    currentDayItem.SetDensityColors(densityColors);
                }
            }
        }

        private void CalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            // Add all the day items loaded
            if (!CalendarViewDayItems.Contains(args.Item))
                CalendarViewDayItems.Add(args.Item);
        }
    }
}

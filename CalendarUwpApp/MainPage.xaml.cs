using DataHelper;
using DataHelper.CalendarItems;
using DataHelper.TaskItems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CalendarUwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            BackStack = new Stack();
            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First(n => n.Tag.Equals("calendar"));

            DataItemHelper.TaskItems = new ObservableCollection<ITaskItem>();
            DataItemHelper.CalendarItems = new ObservableCollection<ICalendarItem>();
        }

        private Stack BackStack { get; set; }
        private readonly List<(string Tag, Type Page)> Pages = new List<(string Tag, Type Page)>
        {
            ("calendar", typeof(Calendar)),
            ("taskList", typeof(TaskList))
        };

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to navigate to page.");
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            // Pop off current page
            BackStack.Pop();

            // Get last page navigated to
            var selectedItem = BackStack.Pop();
            NavView.SelectedItem = selectedItem;
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            BackStack.Push(args.SelectedItem);

            // Check if enough pages to go back
            NavView.IsBackEnabled = BackStack.Count >= 2;

            // Navigate to the current page selected
            var selectedItem = args.SelectedItem as NavigationViewItem;
            Type pageToNavigateTo = args.IsSettingsSelected
                ? typeof(Settings)
                : Pages.First(p => p.Tag.Equals(selectedItem.Tag)).Page;
            ContentFrame.Navigate(pageToNavigateTo);
        }
    }
}

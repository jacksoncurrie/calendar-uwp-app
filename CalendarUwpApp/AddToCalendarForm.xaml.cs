using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CalendarUwpApp
{
    public sealed partial class AddToCalendarForm : UserControl
    {
        public AddToCalendarForm(bool showNameInputs = true, DateTime? startDate = null, DateTime? endDate = null, string itemName = null)
        {
            InitializeComponent();

            ShowNameInputs = showNameInputs;
            if (!showNameInputs)
            {
                ItemNameLabel.Visibility = Visibility.Collapsed;
                ItemNameField.Visibility = Visibility.Collapsed;
            }
            else
            {
                ItemNameField.Text = itemName ?? string.Empty;
            }

            if (startDate != null && endDate != null)
            {
                StartDatePicker.Date = startDate;
                EndDatePicker.Date = endDate;

                if (startDate.Value.TimeOfDay != TimeSpan.Zero && endDate.Value.TimeOfDay != TimeSpan.Zero)
                {
                    StartTimePicker.Time = startDate.Value.TimeOfDay;
                    EndTimePicker.Time = endDate.Value.TimeOfDay;
                }
            }
        }

        private bool ShowNameInputs { get; }

        public bool InputsValid
        {
            get
            {
                var startDateValid = StartDatePicker.Date.HasValue;
                var startTimeValid = StartTimePicker.Time.Ticks != -1;
                var endDateValid = EndDatePicker.Date.HasValue;
                var endTimeValid = EndTimePicker.Time.Ticks != -1;
                var nameValid = ShowNameInputs ? !string.IsNullOrWhiteSpace(ItemNameField.Text) : true;
                return startDateValid && startTimeValid && endDateValid && endTimeValid && nameValid;
            }
        }

        public DateTime StartDateTime
        {
            get
            {
                var startDate = StartDatePicker.Date.Value;
                var startTime = StartTimePicker.Time;
                return new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hours, startTime.Minutes, 0);
            }
        }

        public DateTime EndDateTime
        {
            get
            {
                var endDate = EndDatePicker.Date.Value;
                var endTime = EndTimePicker.Time;
                return new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hours, endTime.Minutes, 0);
            }
        }

        public string ItemName { get => ItemNameField.Text; }
    }
}

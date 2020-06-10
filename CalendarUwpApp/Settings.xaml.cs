using DataHelper;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace CalendarUwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Colours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var colorName = e.AddedItems.First().ToString();
            int index = 0;
            Color color;
            switch (colorName)
            {
                case "Blue":
                    color = Colors.Blue;
                    index = 0;
                    break;

                case "Green":
                    color = Colors.Green;
                    index = 1;
                    break;

                case "Red":
                    color = Colors.Red;
                    index = 2;
                    break;

                case "Yellow":
                    color = Colors.Yellow;
                    index = 3;
                    break;
            }

            DataItemHelper.DensityColor = color;
            DataItemHelper.DensityColorIndex = index;
        }
    }
}

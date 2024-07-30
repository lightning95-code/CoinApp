using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CoinApp.Controls
{
    public partial class TopContentControls : UserControl
    {
        public TopContentControls()
        {
            InitializeComponent();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text == "Search currency by name...")
            {
                searchTextBox.Text = "";
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Search currency by name...";
            }
        }

        private async void Close_This_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            // Отримуємо посилання на вікно, в якому знаходиться UserControl
            Window window = Window.GetWindow(this);

            if (window == null)
                return;

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };

            
            window.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);

           
            await Task.Delay(500);

           
            window.Close();
        }

    }
}

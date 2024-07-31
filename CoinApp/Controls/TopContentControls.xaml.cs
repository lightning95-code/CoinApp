using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CoinApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinApp.Controls
{
    public partial class TopContentControls : UserControl
    {
        private readonly ApiService _apiService;
        private List<string> _currencyNames; // Список всіх назв валют

        public TopContentControls()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadCurrencyNames();
        }

        private async void LoadCurrencyNames()
        {
            try
            {
                var currencies = await _apiService.GetCurrenciesAsync();
                _currencyNames = currencies.Select(c => c.Name).ToList(); // Отримання імен валют
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load currency names: {ex.Message}");
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Отримання введеного тексту
            string searchText = searchTextBox.Text.ToLower();
            if (_currencyNames != null)
            {
                var filteredNames = _currencyNames
                    .Where(name => name.ToLower().Contains(searchText))
                    .ToList();

                // Оновлення ListBox з результатами
                autoCompleteListBox.ItemsSource = filteredNames;
                autoCompletePopup.IsOpen = filteredNames.Count > 0; // Відкриття Popup, якщо є результати
            }
        }

        private void AutoCompleteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBox.SelectedItem is string selectedName)
            {
                searchTextBox.Text = selectedName; // Вставка вибраної назви у TextBox
                autoCompletePopup.IsOpen = false; // Закриття Popup
            }
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

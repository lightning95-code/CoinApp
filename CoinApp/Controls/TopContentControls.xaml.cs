using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CoinApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinApp.Utilities;
using CoinApp.Views;
using System.Globalization;

namespace CoinApp.Controls
{
    public partial class TopContentControls : UserControl
    {
        private readonly ApiService _apiService; // Сервіс для взаємодії з API
        private List<string> _currencyNames; // Список всіх назв валют

        public TopContentControls()
        {
            InitializeComponent();
            _apiService = new ApiService(); // Ініціалізація ApiService
            LoadCurrencyNames(); // Завантаження назв валют
        }

        // Асинхронний метод для завантаження назв валют
        private async void LoadCurrencyNames()
        {
            try
            {
                var currencies = await _apiService.GetCurrenciesAsync(); // Отримання списку валют з API
                _currencyNames = currencies
                    .Select(c => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(c.Id))
                    .ToList(); // Отримання імен валют
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load currency names: {ex.Message}"); // Виведення повідомлення про помилку
            }
        }

        // Обробник зміни тексту в TextBox
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower(); // Отримання введеного тексту в нижньому регістрі

            if (string.IsNullOrEmpty(searchText))
            {
                autoCompletePopup.IsOpen = false;
            }
            else if (_currencyNames != null)
            {
                var filteredNames = _currencyNames
                    .Where(name => name.ToLower().Contains(searchText)) // Фільтрація списку валют
                    .ToList();

                // Оновлення ListBox з результатами
                autoCompleteListBox.ItemsSource = filteredNames;
                autoCompletePopup.IsOpen = filteredNames.Count > 0; // Відкриття Popup, якщо є результати
            }
        }

        // Обробник вибору елемента в ListBox
        private void AutoCompleteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autoCompleteListBox.SelectedItem is string selectedName)
            {
                searchTextBox.Text = selectedName; // Вставка вибраної назви у TextBox
                autoCompletePopup.IsOpen = false; // Закриття Popup
                Go_To_Coin_Page(selectedName); // Перехід на сторінку з інформацією про валюту
            }
        }

        // Обробник отримання фокусу на TextBox
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = "";
        }

        // Обробник втрати фокусу на TextBox
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Search currency by name or select coin..."; // Відновлення тексту при втраті фокусу
            }

            autoCompletePopup.IsOpen = false;
        }

        // Обробник натискання кнопки закриття вікна
        private async void Close_This_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if (window == null) return;

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)) // Налаштування анімації зменшення прозорості
            };

            window.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation); // Запуск анімації

            await Task.Delay(500); // Затримка для завершення анімації

            window.Close(); // Закриття вікна
        }

        // Метод для переходу на сторінку з інформацією про валюту
        private async void Go_To_Coin_Page(string selectedName)
        {
            Window window = Window.GetWindow(this);

            if (window == null)
            {
                MessageBox.Show("Window not found."); // Обробка випадку, коли вікно не знайдене
                return;
            }

            // Збереження стану вікна
            WindowStateManager.Width = window.Width;
            WindowStateManager.Height = window.Height;
            WindowStateManager.Top = window.Top;
            WindowStateManager.Left = window.Left;
            WindowStateManager.IsMaximized = window.WindowState == WindowState.Maximized;

            // Створення нового вікна для перегляду інформації про валюту
            CoinView coin_win = new CoinView(selectedName.ToLower())
            {
                Width = WindowStateManager.Width,
                Height = WindowStateManager.Height,
                Top = WindowStateManager.Top,
                Left = WindowStateManager.Left,
                WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
            };

            coin_win.Show();

            // Додатково, затримка для забезпечення відкриття нового вікна перед закриттям старого
            await Task.Delay(100);

            window.Close(); // Закриття поточного вікна

        }

        // Обробник натискання кнопки для переходу на сторінку з інформацією про валюту
        private void Go_To_Coin_Page_Click(object sender, RoutedEventArgs e)
        {
            Go_To_Coin_Page(searchTextBox.Text); // Передача тексту пошуку у CoinView
        }
    }
}

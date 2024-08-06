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

        private bool IsLanguagePopupOpen = false;

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
                if (TabForSearchBox.Text == "ENG")
                {
                    searchTextBox.Text = "Search currency by name or select coin..."; // Відновлення тексту при втраті фокусу
                }
                else 
                {
                    searchTextBox.Text = "Шукайте валюту за назвою або виберіть її...";
                }      
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
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                Go_To_Coin_Page(searchTextBox.Text); // Передача тексту пошуку у CoinView
            }
            else
            {
                MessageBox.Show("Please, Input correct text!"); 
            }
        }

        private void Change_Lang_Button_Click(object sender, RoutedEventArgs e)
        {
            IsLanguagePopupOpen = !IsLanguagePopupOpen;
            LanguagePopup.IsOpen = IsLanguagePopupOpen;
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Перевірка, чи sender не є null і чи sender є ComboBox
            if (sender is ComboBox comboBox)
            {
                // Перевірка, чи вибраний елемент не є null
                if (comboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    // Перевірка, чи Content вибраного елемента не є null
                    if (selectedItem.Content != null)
                    {
                        // Отримання коду мови з вибраного елемента
                        string languageCode = selectedItem.Content.ToString();

                        // Виклик методу зміни мови
                        ChangeLanguage(languageCode);
                    }
                    else
                    {
                        MessageBox.Show("Selected item content is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No item selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Invalid sender type.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void ChangeLanguage(string languageCode)
        {
            // List of dictionaries that should not be removed
            var nonLocalizationDictionaries = new List<ResourceDictionary>
    {
        new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml") },
        new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign2.Defaults.xaml") },
        new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml") },
        new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Secondary/MaterialDesignColor.Lime.xaml") }
    };

            // Separate the current localization dictionaries
            var localizationDictionaries = Application.Current.Resources.MergedDictionaries
                .Where(dict => !nonLocalizationDictionaries.Any(nonDict => dict.Source == nonDict.Source))
                .ToList();

            // Remove the current localization dictionaries
            foreach (var dictionary in localizationDictionaries)
            {
                Application.Current.Resources.MergedDictionaries.Remove(dictionary);
            }

            // Create and load the new localization resource dictionary
            ResourceDictionary newResourceDictionary = new ResourceDictionary();
            Uri resourceUri = null;

            switch (languageCode.ToUpper())
            {
                case "ENG":
                    resourceUri = new Uri("Resources/lang.xaml", UriKind.Relative);
                    break;
                case "УКР":
                    resourceUri = new Uri("Resources/lang.ukr-UKR.xaml", UriKind.Relative);
                    break;
                default:
                    resourceUri = new Uri("Resources/lang.xaml", UriKind.Relative); // Default to English
                    break;
            }

            if (resourceUri != null)
            {
                try
                {
                    newResourceDictionary.Source = resourceUri;
                    Application.Current.Resources.MergedDictionaries.Add(newResourceDictionary);

                    // Add the non-localization dictionaries back
                    foreach (var dict in nonLocalizationDictionaries)
                    {
                        Application.Current.Resources.MergedDictionaries.Add(dict);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading resource dictionary: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



    }
}

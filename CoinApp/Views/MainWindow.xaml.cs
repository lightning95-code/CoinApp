using CoinApp.Utilities;
using CoinApp.ViewModels;
using CoinApp.Views;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CoinApp
{
    /// <summary>
    /// Логіка взаємодії для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsMaximized = false; // Максимізація вікна
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel; // Встановлюємо DataContext для вікна
        }

        private void Show_Top_10_Currencies_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.ShowingTopCurrencies = true;
                _viewModel.RefreshDataAsync();

                Top_Currencies_button_Icon.Visibility = Visibility.Visible;
                All_Currencies_button.Margin = new Thickness(40, 0, 30, 0);
                All_Currencies_button_Icon.Visibility = Visibility.Collapsed;
            }
        }

        private void Show_All_Currencies_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.ShowingTopCurrencies = false;
                _viewModel.RefreshDataAsync();

                All_Currencies_button_Icon.Visibility = Visibility.Visible;
                All_Currencies_button.Margin = new Thickness(40, 0, 0, 0);
                Top_Currencies_button_Icon.Visibility = Visibility.Collapsed;
            }
        }

        // Максимізація вікна
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }

        // Переміщення вікна при зажатій ЛКМ
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // Оновлення вікна
        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                // Робимо видимою панель завантаження, а таблицю навпаки приховуємо
                LoadingPanel.Visibility = Visibility.Visible;
                CurrenciesDataGrid.Visibility = Visibility.Collapsed;

                await _viewModel.RefreshDataAsync();

                await Task.Delay(2500);

                // Робимо видимою таблицю, а панель навпаки приховуємо
                LoadingPanel.Visibility = Visibility.Collapsed;
                CurrenciesDataGrid.Visibility = Visibility.Visible;
            }
        }

        private async void Go_To_Coin_Page_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Отримуємо ID валюти з тегу кнопки
                string currencyId = button.Tag as string;

                if (!string.IsNullOrEmpty(currencyId))
                {
                    // Збереження стану поточного вікна
                    WindowStateManager.Width = this.Width;
                    WindowStateManager.Height = this.Height;
                    WindowStateManager.Top = this.Top;
                    WindowStateManager.Left = this.Left;
                    WindowStateManager.IsMaximized = this.WindowState == WindowState.Maximized;

                    // Створення нового вікна для перегляду детальної інформації про валюту
                    CoinView coinWin = new CoinView(currencyId)
                    {
                        Width = WindowStateManager.Width,
                        Height = WindowStateManager.Height,
                        Top = WindowStateManager.Top,
                        Left = WindowStateManager.Left,
                        WindowState = WindowStateManager.IsMaximized ? WindowState.Maximized : WindowState.Normal
                    };

                    coinWin.Show();

                    // Додатково, затримка для забезпечення відкриття нового вікна перед закриттям старого
                    await Task.Delay(100);

                    // Закриття поточного вікна
                    this.Close();
                }
                else
                {
                    // Виводимо повідомлення, якщо ID валюти не знайдено
                    MessageBox.Show("Currency ID not found.");
                }
            }
        }
    }
}

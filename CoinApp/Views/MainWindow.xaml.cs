using CoinApp.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace CoinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsMaximized = false; //максимізація вікна
        private CurrenciesViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new CurrenciesViewModel();
            DataContext = _viewModel; //встановлюю дата контекст для вікна
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

        //Максимізація вікна
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

        //Переміщення вікна при зажатій ЛКМ
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        //Оновлення SearchBox
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text == "Search currency by name...")
            {
                searchTextBox.Text = "";
            }
        }
        //Оновлення SearchBox_2
        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Search currency by name...";
            }
        }

        //вихід із програми
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            // Запуск анімації скриття
            DoubleAnimation hideAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            // Додаю анімацію до цього вікна
            this.BeginAnimation(UIElement.OpacityProperty, hideAnimation);

            // Затримка перед завершенням роботи
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5) // тривалість анімації
            };
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                Application.Current.Shutdown();
            };
            timer.Start();
        }

        //Закриття цього вікна
        private async void Close_This_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            // Запуск анімації скриття поточного вікна
            DoubleAnimation hideAnimation = new DoubleAnimation();
            hideAnimation.From = 1.0;
            hideAnimation.To = 0.0;
            hideAnimation.Duration = TimeSpan.FromSeconds(0.5);
            this.BeginAnimation(UIElement.OpacityProperty, hideAnimation);

            await Task.Delay(500);
            this.Close();
        }

        //Оновлення вікна
        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                //Робим видимою панель завантаження, а таблицю навпаки приховуємо
                LoadingPanel.Visibility = Visibility.Visible;
                CurrenciesDataGrid.Visibility = Visibility.Collapsed;

                await _viewModel.RefreshDataAsync();

                await Task.Delay(2500);

                // Робим видимою таблицю, а панель навпаки приховуємо
                LoadingPanel.Visibility = Visibility.Collapsed;
                CurrenciesDataGrid.Visibility = Visibility.Visible;
            }
        }

        private void Main_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}